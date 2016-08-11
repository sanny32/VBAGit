﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Microsoft.Vbe.Interop;
using stdole;
using CommandBarButtonClickEvent = Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler;


namespace VBAGitAddin.UI
{
    public class Menu : IDisposable
    {
        internal class AxHostConverter : AxHost
        {
            private AxHostConverter() : base("") { }

            static public IPictureDisp ImageToPictureDisp(Image image)
            {
                return (IPictureDisp)GetIPictureDispFromPicture(image);
            }

            static public Image PictureDispToImage(IPictureDisp pictureDisp)
            {
                return GetPictureFromIPicture(pictureDisp);
            }
        }

        protected Menu()
        {
        }

        private CommandBarButton AddButton(CommandBarPopup parentMenu, string caption)
        {
            var button = parentMenu.Controls.Add(MsoControlType.msoControlButton, Temporary: true) as CommandBarButton;
            button.Caption = caption;

            return button;
        }

        protected CommandBarButton AddButton(CommandBarPopup parentMenu, string caption, bool beginGroup, CommandBarButtonClickEvent buttonClickHandler)
        {
            var button = AddButton(parentMenu, caption);
            button.BeginGroup = beginGroup;
            button.Click += buttonClickHandler;

            return button;
        }

        protected CommandBarButton AddButton(CommandBarPopup parentMenu, string caption, bool beginGroup, CommandBarButtonClickEvent buttonClickHandler, int faceId)
        {
            var button = AddButton(parentMenu, caption, beginGroup, buttonClickHandler);
            button.FaceId = faceId;

            return button;
        }

        protected CommandBarButton AddButton(CommandBarPopup parentMenu, string caption, bool beginGroup, CommandBarButtonClickEvent buttonClickHandler, string imageName)
        {
            var button = AddButton(parentMenu, caption, beginGroup, buttonClickHandler);
            var resourceCulture = VBAGitAddin.Properties.Resources.Culture;
            Bitmap image = (System.Drawing.Bitmap)VBAGitAddin.Properties.Resources.ResourceManager.GetObject(imageName, resourceCulture);
            Bitmap mask = (System.Drawing.Bitmap)VBAGitAddin.Properties.Resources.ResourceManager.GetObject(imageName + "_mask", resourceCulture);
            SetButtonImage(button, image, mask);
            return button;
        }

        protected CommandBarButton AddButton(CommandBarPopup parentMenu, string caption, bool beginGroup, CommandBarButtonClickEvent buttonClickHandler, Bitmap image)
        {
            var button = AddButton(parentMenu, caption, beginGroup, buttonClickHandler);
            SetButtonImage(button, image);

            return button;
        }

        public static void SetButtonImage(CommandBarButton button, Bitmap image)
        {
            button.FaceId = 0;

            if (image != null)
            {
                image.MakeTransparent(Color.Magenta);
                Clipboard.SetDataObject(image, true);
                button.PasteFace();
            }
        }

        public static void SetButtonImage(CommandBarButton button, Bitmap image, Bitmap mask)
        {
            button.FaceId = 0;
            button.Picture = AxHostConverter.ImageToPictureDisp(image);
            button.Mask = AxHostConverter.ImageToPictureDisp(mask);
        }

        /// <summary>
        /// Finds the index for insertion in a given CommandBarControls collection.
        /// Returns the last position if the given beforeControl caption is not found.
        /// </summary>
        /// <param name="controls">The collection to insert into.</param>
        /// <param name="beforeId">Caption of the control to insert before.</param>
        /// <returns></returns>
        protected int FindMenuInsertionIndex(CommandBarControls controls, int beforeId)
        {
            for (var i = 1; i <= controls.Count; i++)
            {
                if (controls[i].BuiltIn && controls[i].Id == beforeId)
                {
                    return i;
                }
            }

            return controls.Count;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
        }       
    }
}
