﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
// see https://gist.github.com/praeclarum/6225853 (though I've made some modifications from that)
using UIKit;
using Foundation;

namespace Sample
{
    public static class Layout
    {
        // the standard spacing between sibling views
        public static readonly int StandardSiblingViewSpacing = 8;

        // the standard spacing between a view and its superview
        public static readonly int StandardSuperviewSpacing = 20;

        public static readonly float RequiredPriority = 1000;

        public static readonly float HighPriority = 750;

        public static readonly float LowPriority = 250;

        /// <summary>
        /// <para>Constrains the layout of subviews according to equations and
        /// inequalities specified in <paramref name="constraints"/>.  Issue
        /// multiple constraints per call using the &amp;&amp; operator.</para>
        /// <para>e.g. button.Frame.Left &gt;= text.Frame.Right + 22 &amp;&amp;
        /// button.Frame.Width == View.Frame.Width * 0.42f</para>
        /// </summary>
        /// <param name="view">The superview laying out the referenced subviews.</param>
        /// <param name="constraints">Constraint equations and inequalities.</param>
        public static NSLayoutConstraint[] ConstrainLayout(this UIView view, Expression<Func<bool>> constraints)
        {
            var body = ((LambdaExpression)constraints).Body;

            var exprs = new List<BinaryExpression>();
            FindConstraints(body, exprs);

            var newConstraints = exprs.Select(e => CompileConstraint(e, view)).ToArray();
            view.AddConstraints(newConstraints);

            return newConstraints;
        }

        static NSLayoutConstraint CompileConstraint(BinaryExpression expr, UIView constrainedView)
        {
            var rel = NSLayoutRelation.Equal;
            switch (expr.NodeType)
            {
                case ExpressionType.Equal:
                    rel = NSLayoutRelation.Equal;
                    break;
                case ExpressionType.LessThanOrEqual:
                    rel = NSLayoutRelation.LessThanOrEqual;
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    rel = NSLayoutRelation.GreaterThanOrEqual;
                    break;
                default:
                    throw new NotSupportedException("Not a valid relationship for a constrain.");
            }

            var left = GetViewAndAttribute(expr.Left);
            if (left.Item1 != constrainedView)
                left.Item1.TranslatesAutoresizingMaskIntoConstraints = false;

            var right = GetRight(expr.Right);
            if (right.Item1 != null && right.Item1 != constrainedView)
                right.Item1.TranslatesAutoresizingMaskIntoConstraints = false;

            return NSLayoutConstraint.Create(
                left.Item1, left.Item2,
                rel,
                right.Item1, right.Item2,
                right.Item3, right.Item4);
        }

        static Tuple<UIView, NSLayoutAttribute, float, float> GetRight(Expression expr)
        {
            var r = expr;

            UIView view = null;
            NSLayoutAttribute attr = NSLayoutAttribute.NoAttribute;
            var mul = 1.0f;
            var add = 0.0f;
            var pos = true;

            if (r.NodeType == ExpressionType.Add || r.NodeType == ExpressionType.Subtract)
            {
                var rb = (BinaryExpression)r;
                if (IsConstant(rb.Left))
                {
                    add = ConstantValue(rb.Left);
                    if (r.NodeType == ExpressionType.Subtract)
                    {
                        pos = false;
                    }
                    r = rb.Right;
                }
                else if (IsConstant(rb.Right))
                {
                    add = ConstantValue(rb.Right);
                    if (r.NodeType == ExpressionType.Subtract)
                    {
                        add = -add;
                    }
                    r = rb.Left;
                }
                else
                {
                    throw new NotSupportedException("Addition only supports constants: " + rb.Right.NodeType);
                }
            }

            if (r.NodeType == ExpressionType.Multiply)
            {
                var rb = (BinaryExpression)r;
                if (IsConstant(rb.Left))
                {
                    mul = ConstantValue(rb.Left);
                    r = rb.Right;
                }
                else if (IsConstant(rb.Right))
                {
                    mul = ConstantValue(rb.Right);
                    r = rb.Left;
                }
                else
                {
                    throw new NotSupportedException("Multiplication only supports constants.");
                }
            }

            if (IsConstant(r))
            {
                add = Convert.ToSingle(Eval(r));
            }
            else if (r.NodeType == ExpressionType.MemberAccess || r.NodeType == ExpressionType.Call)
            {
                var t = GetViewAndAttribute(r);
                view = t.Item1;
                attr = t.Item2;
            }
            else
            {
                throw new NotSupportedException("Unsupported layout expression node type " + r.NodeType);
            }

            if (!pos)
                mul = -mul;

            return Tuple.Create(view, attr, mul, add);
        }

        static bool IsConstant(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Constant)
                return true;

            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                var mexpr = (MemberExpression)expr;
                var m = mexpr.Member;
                if (m.MemberType == MemberTypes.Field)
                {
                    var f = (FieldInfo)m;
                    return f.IsStatic;
                }
                return false;
            }
            return false;
        }

        static float ConstantValue(Expression expr)
        {
            return Convert.ToSingle(Eval(expr));
        }

        static Tuple<UIView, NSLayoutAttribute> GetViewAndAttribute(Expression expr)
        {
            var attr = NSLayoutAttribute.NoAttribute;
            Expression viewExpression = null;

            var fExpr = expr as MethodCallExpression;
            if (fExpr != null)
            {
                switch (fExpr.Method.Name)
                {
                    case "Width":
                    case "WidthFloat":
                        attr = NSLayoutAttribute.Width;
                        break;
                    case "Height":
                    case "HeightFloat":
                        attr = NSLayoutAttribute.Height;
                        break;
                    case "Left":
                    case "X":
                    case "LeftFloat":
                    case "XFloat":
                        attr = NSLayoutAttribute.Left;
                        break;
                    case "Top":
                    case "Y":
                    case "TopFloat":
                    case "YFloat":
                        attr = NSLayoutAttribute.Top;
                        break;
                    case "Right":
                    case "RightFloat":
                        attr = NSLayoutAttribute.Right;
                        break;
                    case "Bottom":
                    case "BottomFloat":
                        attr = NSLayoutAttribute.Bottom;
                        break;
                    case "CenterX":
                    case "CenterXFloat":
                        attr = NSLayoutAttribute.CenterX;
                        break;
                    case "CenterY":
                    case "CenterYFloat":
                        attr = NSLayoutAttribute.CenterY;
                        break;
                    case "Baseline":
                    case "BaselineFloat":
                        attr = NSLayoutAttribute.Baseline;
                        break;
                    case "Leading":
                    case "LeadingFloat":
                        attr = NSLayoutAttribute.Leading;
                        break;
                    case "Trailing":
                    case "TrailingFloat":
                        attr = NSLayoutAttribute.Trailing;
                        break;
                    default:
                        throw new NotSupportedException("Method " + fExpr.Method.Name + " is not recognized as a constraint.");
                }

                viewExpression = fExpr.Arguments.FirstOrDefault();
            }

            if (viewExpression == null)
                throw new NotSupportedException("Constraint expression not found.");

            var view = Eval(viewExpression) as UIView;
            if (view == null)
                throw new NotSupportedException("Constraints only apply to views.");

            return Tuple.Create(view, attr);
        }

        static object Eval(Expression expr)
        {
            if (expr.NodeType == ExpressionType.Constant)
            {
                return ((ConstantExpression)expr).Value;
            }

            if (expr.NodeType == ExpressionType.MemberAccess)
            {
                var mexpr = (MemberExpression)expr;
                var m = mexpr.Member;
                if (m.MemberType == MemberTypes.Field)
                {
                    var f = (FieldInfo)m;
                    if (f.IsStatic)
                        return f.GetValue(null);
                }
            }

            return Expression.Lambda(expr).Compile().DynamicInvoke();
        }

        static void FindConstraints(Expression expr, List<BinaryExpression> constraintExprs)
        {
            var b = expr as BinaryExpression;
            if (b == null)
                return;

            if (b.NodeType == ExpressionType.AndAlso)
            {
                FindConstraints(b.Left, constraintExprs);
                FindConstraints(b.Right, constraintExprs);
            }
            else
            {
                constraintExprs.Add(b);
            }
        }
    }

    // note the use of ints here rather than floats because comparing floats in our constraint expressions results in compiler warnings
    public static class LayoutEx
    {
        public static int Width(this UIView @this)
        {
            return 0;
        }

        public static int Height(this UIView @this)
        {
            return 0;
        }

        public static int Left(this UIView @this)
        {
            return 0;
        }

        public static int X(this UIView @this)
        {
            return 0;
        }

        public static int Top(this UIView @this)
        {
            return 0;
        }

        public static int Y(this UIView @this)
        {
            return 0;
        }

        public static int Right(this UIView @this)
        {
            return 0;
        }

        public static int Bottom(this UIView @this)
        {
            return 0;
        }

        public static int Baseline(this UIView @this)
        {
            return 0;
        }

        public static int Leading(this UIView @this)
        {
            return 0;
        }

        public static int Trailing(this UIView @this)
        {
            return 0;
        }

        public static int CenterX(this UIView @this)
        {
            return 0;
        }

        public static int CenterY(this UIView @this)
        {
            return 0;
        }
        public static float WidthFloat(this UIView @this)
        {
            return 0;
        }

        public static float HeightFloat(this UIView @this)
        {
            return 0;
        }

        public static float LeftFloat(this UIView @this)
        {
            return 0;
        }

        public static float XFloat(this UIView @this)
        {
            return 0;
        }

        public static float TopFloat(this UIView @this)
        {
            return 0;
        }

        public static float YFloat(this UIView @this)
        {
            return 0;
        }

        public static float RightFloat(this UIView @this)
        {
            return 0;
        }

        public static float BottomFloat(this UIView @this)
        {
            return 0;
        }

        public static float BaselineFloat(this UIView @this)
        {
            return 0;
        }

        public static float LeadingFloat(this UIView @this)
        {
            return 0;
        }

        public static float TrailingFloat(this UIView @this)
        {
            return 0;
        }

        public static float CenterXFloat(this UIView @this)
        {
            return 0;
        }

        public static float CenterYFloat(this UIView @this)
        {
            return 0;
        }
    }

    public static class AttributedStringEx
    {
        public static NSAttributedString ToAttributedString(this string html)
        {
            return new NSMutableAttributedString(html);
        }
    }

    public static class Extensions
    {
        public static NSLayoutConstraint MaintainAspect(this UIImageView imageView, float aspect)
        {
            var aspectRatioConstraint = NSLayoutConstraint.Create(
                imageView,
                NSLayoutAttribute.Height,
                NSLayoutRelation.Equal,
                imageView,
                NSLayoutAttribute.Width,
                aspect,
                0
            );
            aspectRatioConstraint.Priority = Layout.RequiredPriority;

            imageView.AddConstraint(aspectRatioConstraint);
            return aspectRatioConstraint;
        }

        public static NSLayoutConstraint[] MatchParentFrame(this UIView childView, UIView parentView = null)
        {
            if (parentView == null)
                parentView = childView.Superview;

            return parentView.ConstrainLayout(() =>
                childView.Left() == parentView.Left() &&
                childView.Top() == parentView.Top() &&
                childView.Height() == parentView.Height() &&
                childView.Width() == parentView.Width()
            );
        }

        public static NSLayoutConstraint[] MatchSiblingFrame(this UIView current, UIView sibling)
        {
            return current.Superview.ConstrainLayout(() =>
                current.Left() == sibling.Left() &&
                current.Top() == sibling.Top() &&
                current.Right() == sibling.Right() &&
                current.Bottom() == sibling.Bottom()
            );
        }

        public static NSLayoutConstraint[] CenterInParent(this UIView childView, UIView parentView = null)
        {
            if (parentView == null)
                parentView = childView.Superview;

            return parentView.ConstrainLayout(() =>
                childView.CenterX() == parentView.CenterX() &&
                childView.CenterY() == parentView.CenterY()    
            );
        }
    }

}
