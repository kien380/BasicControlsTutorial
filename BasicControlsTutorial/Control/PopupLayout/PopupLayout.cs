﻿// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="PopupLayout.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using Xamarin.Forms;

namespace BasicControlsTutorial.Control
{
    /// <summary>
    ///     Class PopupLayout.
    /// </summary>
    public class PopupLayout : ContentView
    {
        /// <summary>
        /// Popup location options when relative to another view
        /// </summary>
        public enum PopupLocation
        {
            /// <summary>
            ///     Will show popup on top of the specified view
            /// </summary>
            Top,

            /// <summary>
            ///     Will show popup below of the specified view
            /// </summary>
            Bottom
       
            //Left,
      
            //Right
        }

        /// <summary>
        /// The content
        /// </summary>
        private View content;

        /// <summary>
        /// The popup
        /// </summary>
        private View popup;

        private readonly RelativeLayout layout;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopupLayout"/> class.
        /// </summary>
        public PopupLayout()
        {
            base.Content = this.layout = new RelativeLayout();
            this.popup = null; 
        }

		#region Content property

		public new static BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(PopupLayout),
																					 null, propertyChanged: OnContentChanged);

		private void SetContent(View view)
		{
			if (view != null)
				layout.Children.Remove(view);
			content = view;
			if (content != null)
				layout.Children.Add(content, () => Bounds);
		}

		private static void OnContentChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var obj = bindable as PopupLayout;
			obj.SetContent(newValue as View);
		}

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public new View Content
        {
			get
			{
				return GetValue(ContentProperty) as View;
			}
			set
			{
				SetValue(ContentProperty, value);
			}
        }

		#endregion

        /// <summary>
        /// Gets a value indicating whether this instance is popup active.
        /// </summary>
        /// <value><c>true</c> if this instance is popup active; otherwise, <c>false</c>.</value>
        public bool IsPopupActive
        {
            get { return this.popup != null; }
        }

        /// <summary>
        /// Shows the popup centered to the parent view.
        /// </summary>
        /// <param name="popupView">The popup view.</param>
        public void ShowPopup(View popupView)
        {
            this.ShowPopup(
                popupView,
                Constraint.RelativeToParent(p => (this.Width - this.popup.WidthRequest) / 2),
                Constraint.RelativeToParent(p => (this.Height- this.popup.HeightRequest) / 2)
                );
        }

        /// <summary>
        /// Shows the popup with constraints.
        /// </summary>
        /// <param name="popupView">The popup view.</param>
        /// <param name="xConstraint">X constraint.</param>
        /// <param name="yConstraint">Y constraint.</param>
        /// <param name="widthConstraint">Optional width constraint.</param>
        /// <param name="heightConstraint">Optional height constraint.</param>
        public void ShowPopup(View popupView, Constraint xConstraint, Constraint yConstraint, Constraint widthConstraint = null, Constraint heightConstraint = null)
        {
            DismissPopup();
            this.popup = popupView;

			if (this.content != null)
				this.content.InputTransparent = true;
            this.layout.Children.Add(this.popup, xConstraint, yConstraint, widthConstraint, heightConstraint);

            this.layout.ForceLayout();

            // Add animation when show Popup
            var scaleUpAnimation = new Animation(v => this.popup.Scale = v, 0, 1, Easing.SpringIn);
            scaleUpAnimation.Commit(this, "Scale Up Animation");
        }
        

        /// <summary>
        /// Shows the popup.
        /// </summary>
        /// <param name="popupView">The popup view.</param>
        /// <param name="presenter">The presenter.</param>
        /// <param name="location">The location.</param>
        /// <param name="paddingX">The padding x.</param>
        /// <param name="paddingY">The padding y.</param>
        public void ShowPopup(View popupView, View presenter, PopupLocation location, float paddingX = 0, float paddingY = 0)
        {
            DismissPopup();
            this.popup = popupView;

            Constraint constraintX = null, constraintY = null;

            switch (location)
            {
                case PopupLocation.Bottom:
                    constraintX = Constraint.RelativeToParent(parent => presenter.X + (presenter.Width - this.popup.WidthRequest)/2);
                    constraintY = Constraint.RelativeToParent(parent => parent.Y + presenter.Y + presenter.Height + paddingY);
                    break;
                case PopupLocation.Top:
                    constraintX = Constraint.RelativeToParent(parent => presenter.X + (presenter.Width - this.popup.WidthRequest)/2);
                    constraintY = Constraint.RelativeToParent(parent =>
                        parent.Y + presenter.Y - this.popup.HeightRequest/2 - paddingY);
                    break;
                //case PopupLocation.Left:
                //    constraintX = Constraint.RelativeToView(presenter, (parent, view) => ((view.X + view.Height / 2) - parent.X) + this.popup.HeightRequest / 2);
                //    constraintY = Constraint.RelativeToView(presenter, (parent, view) => parent.Y + view.Y + view.Width + paddingY);
                //    break;
                //case PopupLocation.Right:
                //    constraintX = Constraint.RelativeToView(presenter, (parent, view) => ((view.X + view.Height / 2) - parent.X) + this.popup.HeightRequest / 2);
                //    constraintY = Constraint.RelativeToView(presenter, (parent, view) => parent.Y + view.Y - this.popup.WidthRequest - paddingY);
                //    break;
            }

            this.ShowPopup(popupView, constraintX, constraintY);
        }

        /// <summary>
        /// Dismisses the popup.
        /// </summary>
        public void DismissPopup()
        {

            if (this.popup != null)
            {
                /*
                // Add animation when dismiss popup
                var scaleDownAnimation = new Animation(v => this.layout.Scale = v, 1, 0, Easing.SpringOut);
                scaleDownAnimation.Commit(this, "Scale Down Animation");*/

                this.layout.Children.Remove(this.popup);
                this.popup = null;
            }
                
            this.layout.InputTransparent = false;

            if (this.content != null)
            {
                this.content.InputTransparent = false;
            }
        }
    }
}
