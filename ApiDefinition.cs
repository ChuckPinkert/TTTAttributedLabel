﻿using System;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace AttributedLabel
{
    partial interface Constants
    {
        // extern NSString *const kTTTStrikeOutAttributeName;
        [Field ("kTTTStrikeOutAttributeName")]
        NSString kTTTStrikeOutAttributeName { get; }

        // extern NSString *const kTTTBackgroundFillColorAttributeName;
        [Field ("kTTTBackgroundFillColorAttributeName")]
        NSString kTTTBackgroundFillColorAttributeName { get; }

        // extern NSString *const kTTTBackgroundFillPaddingAttributeName;
        [Field ("kTTTBackgroundFillPaddingAttributeName")]
        NSString kTTTBackgroundFillPaddingAttributeName { get; }

        // extern NSString *const kTTTBackgroundStrokeColorAttributeName;
        [Field ("kTTTBackgroundStrokeColorAttributeName")]
        NSString kTTTBackgroundStrokeColorAttributeName { get; }

        // extern NSString *const kTTTBackgroundLineWidthAttributeName;
        [Field ("kTTTBackgroundLineWidthAttributeName")]
        NSString kTTTBackgroundLineWidthAttributeName { get; }

        // extern NSString *const kTTTBackgroundCornerRadiusAttributeName;
        [Field ("kTTTBackgroundCornerRadiusAttributeName")]
        NSString kTTTBackgroundCornerRadiusAttributeName { get; }
    }
        

    // @interface TTTAttributedLabel : UILabel <TTTAttributedLabel, UIGestureRecognizerDelegate>
    [BaseType (typeof(UILabel))]
    [Protocol]
    interface TTTAttributedLabel : IUIGestureRecognizerDelegate
    {
        [Wrap ("WeakDelegate")]
        TTTAttributedLabelDelegate Delegate { get; set; }

        // @property (nonatomic, unsafe_unretained) id<TTTAttributedLabelDelegate> delegate;
        [NullAllowed, Export ("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // @property (assign, nonatomic) NSTextCheckingTypes dataDetectorTypes;
        [Export ("dataDetectorTypes")]
        ulong DataDetectorTypes { get; set; }

        // @property (assign, nonatomic) NSTextCheckingTypes enabledTextCheckingTypes;
        [Export ("enabledTextCheckingTypes")]
        ulong EnabledTextCheckingTypes { get; set; }

        // @property (readonly, nonatomic, strong) NSArray * links;
        [Export ("links", ArgumentSemantic.Strong)]
        NSObject[] Links { get; }

        // @property (nonatomic, strong) NSDictionary * linkAttributes;
        [Export ("linkAttributes", ArgumentSemantic.Strong)]
        NSDictionary LinkAttributes { get; set; }

        // @property (nonatomic, strong) NSDictionary * activeLinkAttributes;
        [Export ("activeLinkAttributes", ArgumentSemantic.Strong)]
        NSDictionary ActiveLinkAttributes { get; set; }

        // @property (nonatomic, strong) NSDictionary * inactiveLinkAttributes;
        [Export ("inactiveLinkAttributes", ArgumentSemantic.Strong)]
        NSDictionary InactiveLinkAttributes { get; set; }

        // @property (assign, nonatomic) UIEdgeInsets linkBackgroundEdgeInset;
        [Export ("linkBackgroundEdgeInset", ArgumentSemantic.Assign)]
        UIEdgeInsets LinkBackgroundEdgeInset { get; set; }

        // @property (assign, nonatomic) BOOL extendsLinkTouchArea;
        [Export ("extendsLinkTouchArea")]
        bool ExtendsLinkTouchArea { get; set; }

        // @property (assign, nonatomic) CGFloat shadowRadius;
        [Export ("shadowRadius", ArgumentSemantic.Assign)]
        nfloat ShadowRadius { get; set; }

        // @property (assign, nonatomic) CGFloat highlightedShadowRadius;
        [Export ("highlightedShadowRadius", ArgumentSemantic.Assign)]
        nfloat HighlightedShadowRadius { get; set; }

        // @property (assign, nonatomic) CGSize highlightedShadowOffset;
        [Export ("highlightedShadowOffset", ArgumentSemantic.Assign)]
        CGSize HighlightedShadowOffset { get; set; }

        // @property (nonatomic, strong) UIColor * highlightedShadowColor;
        [Export ("highlightedShadowColor", ArgumentSemantic.Strong)]
        UIColor HighlightedShadowColor { get; set; }

        // @property (assign, nonatomic) CGFloat kern;
        [Export ("kern", ArgumentSemantic.Assign)]
        nfloat Kern { get; set; }

        // @property (assign, nonatomic) CGFloat firstLineIndent;
        [Export ("firstLineIndent", ArgumentSemantic.Assign)]
        nfloat FirstLineIndent { get; set; }

        // @property (assign, nonatomic) CGFloat leading;
        [Export ("leading", ArgumentSemantic.Assign)]
        nfloat Leading { get; set; }

        // @property (assign, nonatomic) CGFloat lineSpacing;
        [Export ("lineSpacing", ArgumentSemantic.Assign)]
        nfloat LineSpacing { get; set; }

        // @property (assign, nonatomic) CGFloat minimumLineHeight;
        [Export ("minimumLineHeight", ArgumentSemantic.Assign)]
        nfloat MinimumLineHeight { get; set; }

        // @property (assign, nonatomic) CGFloat maximumLineHeight;
        [Export ("maximumLineHeight", ArgumentSemantic.Assign)]
        nfloat MaximumLineHeight { get; set; }

        // @property (assign, nonatomic) CGFloat lineHeightMultiple;
        [Export ("lineHeightMultiple", ArgumentSemantic.Assign)]
        nfloat LineHeightMultiple { get; set; }

        // @property (assign, nonatomic) UIEdgeInsets textInsets;
        [Export ("textInsets", ArgumentSemantic.Assign)]
        UIEdgeInsets TextInsets { get; set; }

        // @property (assign, nonatomic) TTTAttributedLabelVerticalAlignment verticalAlignment;
        [Export ("verticalAlignment", ArgumentSemantic.Assign)]
        TTTAttributedLabelVerticalAlignment VerticalAlignment { get; set; }

        // @property (nonatomic, strong) NSString * truncationTokenString;
        [Export ("truncationTokenString", ArgumentSemantic.Strong)]
        string TruncationTokenString { get; set; }

        // @property (nonatomic, strong) NSDictionary * truncationTokenStringAttributes;
        [Export ("truncationTokenStringAttributes", ArgumentSemantic.Strong)]
        NSDictionary TruncationTokenStringAttributes { get; set; }

        // @property (nonatomic, strong) NSAttributedString * attributedTruncationToken;
        [Export ("attributedTruncationToken", ArgumentSemantic.Strong)]
        NSAttributedString AttributedTruncationToken { get; set; }

        // @property (readonly, nonatomic, strong) UILongPressGestureRecognizer * longPressGestureRecognizer;
        [Export ("longPressGestureRecognizer", ArgumentSemantic.Strong)]
        UILongPressGestureRecognizer LongPressGestureRecognizer { get; }

        // +(CGSize)sizeThatFitsAttributedString:(NSAttributedString *)attributedString withConstraints:(CGSize)size limitedToNumberOfLines:(NSUInteger)numberOfLines;
        [Static]
        [Export ("sizeThatFitsAttributedString:withConstraints:limitedToNumberOfLines:")]
        CGSize SizeThatFitsAttributedString (NSAttributedString attributedString, CGSize size, nuint numberOfLines);

        // -(void)setText:(id)text;
        [Export ("setText:")]
        void SetText (NSObject text);

        // -(void)setText:(id)text afterInheritingLabelAttributesAndConfiguringWithBlock:(NSMutableAttributedString *(^)(NSMutableAttributedString *))block;
        [Export ("setText:afterInheritingLabelAttributesAndConfiguringWithBlock:")]
        void SetText (NSObject text, Func<NSMutableAttributedString, NSMutableAttributedString> block);

        // @property (readwrite, copy, nonatomic) NSAttributedString * attributedText;
        [Export ("attributedText", ArgumentSemantic.Copy)]
        NSAttributedString AttributedText { get; set; }

        // -(void)addLink:(TTTAttributedLabelLink *)link;
        [Export ("addLink:")]
        void AddLink (TTTAttributedLabelLink link);

        // -(TTTAttributedLabelLink *)addLinkWithTextCheckingResult:(NSTextCheckingResult *)result;
        [Export ("addLinkWithTextCheckingResult:")]
        TTTAttributedLabelLink AddLinkWithTextCheckingResult (NSObject result);

        // -(TTTAttributedLabelLink *)addLinkWithTextCheckingResult:(NSTextCheckingResult *)result attributes:(NSDictionary *)attributes;
        [Export ("addLinkWithTextCheckingResult:attributes:")]
        TTTAttributedLabelLink AddLinkWithTextCheckingResult (NSObject result, NSDictionary attributes);

        // -(TTTAttributedLabelLink *)addLinkToURL:(NSURL *)url withRange:(NSRange)range;
        [Export ("addLinkToURL:withRange:")]
        TTTAttributedLabelLink AddLinkToURL (NSUrl url, NSRange range);

        // -(TTTAttributedLabelLink *)addLinkToAddress:(NSDictionary *)addressComponents withRange:(NSRange)range;
        [Export ("addLinkToAddress:withRange:")]
        TTTAttributedLabelLink AddLinkToAddress (NSDictionary addressComponents, NSRange range);

        // -(TTTAttributedLabelLink *)addLinkToPhoneNumber:(NSString *)phoneNumber withRange:(NSRange)range;
        [Export ("addLinkToPhoneNumber:withRange:")]
        TTTAttributedLabelLink AddLinkToPhoneNumber (string phoneNumber, NSRange range);

        // -(TTTAttributedLabelLink *)addLinkToDate:(NSDate *)date withRange:(NSRange)range;
        [Export ("addLinkToDate:withRange:")]
        TTTAttributedLabelLink AddLinkToDate (NSDate date, NSRange range);

        // -(TTTAttributedLabelLink *)addLinkToDate:(NSDate *)date timeZone:(NSTimeZone *)timeZone duration:(NSTimeInterval)duration withRange:(NSRange)range;
        [Export ("addLinkToDate:timeZone:duration:withRange:")]
        TTTAttributedLabelLink AddLinkToDate (NSDate date, NSTimeZone timeZone, double duration, NSRange range);

        // -(TTTAttributedLabelLink *)addLinkToTransitInformation:(NSDictionary *)components withRange:(NSRange)range;
        [Export ("addLinkToTransitInformation:withRange:")]
        TTTAttributedLabelLink AddLinkToTransitInformation (NSDictionary components, NSRange range);

        // -(BOOL)containslinkAtPoint:(CGPoint)point;
        [Export ("containslinkAtPoint:")]
        bool ContainslinkAtPoint (CGPoint point);
    }

    // @protocol TTTAttributedLabelDelegate <NSObject>
    [Protocol, Model]
    [BaseType (typeof(NSObject))]
    interface TTTAttributedLabelDelegate
    {
        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didSelectLinkWithURL:(NSURL *)url;
        [Export ("attributedLabel:didSelectLinkWithURL:")]
        void DidSelectLinkWithURL (TTTAttributedLabel label, NSUrl url);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didSelectLinkWithAddress:(NSDictionary *)addressComponents;
        [Export ("attributedLabel:didSelectLinkWithAddress:")]
        void DidSelectLinkWithAddress (TTTAttributedLabel label, NSDictionary addressComponents);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didSelectLinkWithPhoneNumber:(NSString *)phoneNumber;
        [Export ("attributedLabel:didSelectLinkWithPhoneNumber:")]
        void DidSelectLinkWithPhoneNumber (TTTAttributedLabel label, string phoneNumber);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didSelectLinkWithDate:(NSDate *)date;
        [Export ("attributedLabel:didSelectLinkWithDate:")]
        void DidSelectLinkWithDate (TTTAttributedLabel label, NSDate date);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didSelectLinkWithDate:(NSDate *)date timeZone:(NSTimeZone *)timeZone duration:(NSTimeInterval)duration;
        [Export ("attributedLabel:didSelectLinkWithDate:timeZone:duration:")]
        void DidSelectLinkWithDate (TTTAttributedLabel label, NSDate date, NSTimeZone timeZone, double duration);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didSelectLinkWithTransitInformation:(NSDictionary *)components;
        [Export ("attributedLabel:didSelectLinkWithTransitInformation:")]
        void DidSelectLinkWithTransitInformation (TTTAttributedLabel label, NSDictionary components);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didSelectLinkWithTextCheckingResult:(NSTextCheckingResult *)result;
        [Export ("attributedLabel:didSelectLinkWithTextCheckingResult:")]
        void DidSelectLinkWithTextCheckingResult (TTTAttributedLabel label, NSObject result);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didLongPressLinkWithURL:(NSURL *)url atPoint:(CGPoint)point;
        [Export ("attributedLabel:didLongPressLinkWithURL:atPoint:")]
        void DidLongPressLinkWithURL (TTTAttributedLabel label, NSUrl url, CGPoint point);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didLongPressLinkWithAddress:(NSDictionary *)addressComponents atPoint:(CGPoint)point;
        [Export ("attributedLabel:didLongPressLinkWithAddress:atPoint:")]
        void DidLongPressLinkWithAddress (TTTAttributedLabel label, NSDictionary addressComponents, CGPoint point);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didLongPressLinkWithPhoneNumber:(NSString *)phoneNumber atPoint:(CGPoint)point;
        [Export ("attributedLabel:didLongPressLinkWithPhoneNumber:atPoint:")]
        void DidLongPressLinkWithPhoneNumber (TTTAttributedLabel label, string phoneNumber, CGPoint point);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didLongPressLinkWithDate:(NSDate *)date atPoint:(CGPoint)point;
        [Export ("attributedLabel:didLongPressLinkWithDate:atPoint:")]
        void DidLongPressLinkWithDate (TTTAttributedLabel label, NSDate date, CGPoint point);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didLongPressLinkWithDate:(NSDate *)date timeZone:(NSTimeZone *)timeZone duration:(NSTimeInterval)duration atPoint:(CGPoint)point;
        [Export ("attributedLabel:didLongPressLinkWithDate:timeZone:duration:atPoint:")]
        void DidLongPressLinkWithDate (TTTAttributedLabel label, NSDate date, NSTimeZone timeZone, double duration, CGPoint point);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didLongPressLinkWithTransitInformation:(NSDictionary *)components atPoint:(CGPoint)point;
        [Export ("attributedLabel:didLongPressLinkWithTransitInformation:atPoint:")]
        void DidLongPressLinkWithTransitInformation (TTTAttributedLabel label, NSDictionary components, CGPoint point);

        // @optional -(void)attributedLabel:(TTTAttributedLabel *)label didLongPressLinkWithTextCheckingResult:(NSTextCheckingResult *)result atPoint:(CGPoint)point;
        [Export ("attributedLabel:didLongPressLinkWithTextCheckingResult:atPoint:")]
        void DidLongPressLinkWithTextCheckingResult (TTTAttributedLabel label, NSObject result, CGPoint point);
    }

    // @interface TTTAttributedLabelLink : NSObject <NSCoding>
    [BaseType (typeof(NSObject))]
    [Protocol]
    interface TTTAttributedLabelLink : INSCoding
    {
        // @property (readonly, nonatomic, strong) NSTextCheckingResult * result;
        [Export ("result", ArgumentSemantic.Strong)]
        NSObject Result { get; }

        // @property (readonly, copy, nonatomic) NSDictionary * attributes;
        [Export ("attributes", ArgumentSemantic.Copy)]
        NSDictionary Attributes { get; }

        // @property (readonly, copy, nonatomic) NSDictionary * activeAttributes;
        [Export ("activeAttributes", ArgumentSemantic.Copy)]
        NSDictionary ActiveAttributes { get; }

        // @property (readonly, copy, nonatomic) NSDictionary * inactiveAttributes;
        [Export ("inactiveAttributes", ArgumentSemantic.Copy)]
        NSDictionary InactiveAttributes { get; }

        // @property (copy, nonatomic) NSString * accessibilityValue;
        [Export ("accessibilityValue")]
        string AccessibilityValue { get; set; }

        // @property (copy, nonatomic) TTTAttributedLabelLinkBlock linkTapBlock;
        [Export ("linkTapBlock", ArgumentSemantic.Copy)]
        TTTAttributedLabelLinkBlock LinkTapBlock { get; set; }

        // @property (copy, nonatomic) TTTAttributedLabelLinkBlock linkLongPressBlock;
        [Export ("linkLongPressBlock", ArgumentSemantic.Copy)]
        TTTAttributedLabelLinkBlock LinkLongPressBlock { get; set; }

        // -(instancetype)initWithAttributes:(NSDictionary *)attributes activeAttributes:(NSDictionary *)activeAttributes inactiveAttributes:(NSDictionary *)inactiveAttributes textCheckingResult:(NSTextCheckingResult *)result;
        [Export ("initWithAttributes:activeAttributes:inactiveAttributes:textCheckingResult:")]
        IntPtr Constructor (NSDictionary attributes, NSDictionary activeAttributes, NSDictionary inactiveAttributes, NSObject result);

        // -(instancetype)initWithAttributesFromLabel:(TTTAttributedLabel *)label textCheckingResult:(NSTextCheckingResult *)result;
        [Export ("initWithAttributesFromLabel:textCheckingResult:")]
        IntPtr Constructor (TTTAttributedLabel label, NSObject result);
    }

    // typedef void (^TTTAttributedLabelLinkBlock)(TTTAttributedLabel *TTTAttributedLabelLink *);
    delegate void TTTAttributedLabelLinkBlock (TTTAttributedLabel arg0, TTTAttributedLabelLink arg1);
}
