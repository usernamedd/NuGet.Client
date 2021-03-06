// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using NuGet.VisualStudio;

namespace NuGet.PackageManagement.UI
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class LicenseAcceptanceWindow : VsDialogWindow
    {

        private const int MinColumnWidth = 400;
        private const int MinAdditionalColumnWidthWithMargin = 434;  

        public LicenseAcceptanceWindow()
        {
            InitializeComponent();
        }

        private void OnViewLicenseTermsRequestNavigate(object sender, RoutedEventArgs e)
        {
            var hyperlink = (Hyperlink)sender;
            if (hyperlink != null
                && hyperlink.NavigateUri != null)
            {
                UIUtility.LaunchExternalLink(hyperlink.NavigateUri);
                e.Handled = true;
            }
        }

        private void ViewLicense_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink hyperlink)
            {
                if (hyperlink.DataContext is LicenseFileText licenseFile)
                {
                    if (EmbeddedLicense.Visibility == Visibility.Collapsed)
                    {
                        LicenseFileColumn.Width = new GridLength(1, GridUnitType.Star);
                        LicenseFileColumn.MinWidth = MinColumnWidth; // Make both columns the same min width
                        Width += MinAdditionalColumnWidthWithMargin; // Change the width to account for the added textbox
                        MinWidth += MinAdditionalColumnWidthWithMargin; // Change the min width to account for the added textbox
                        EmbeddedLicense.Visibility = Visibility.Visible;
                        EmbeddedLicenseHeader.Visibility = Visibility.Visible;
                    }
                    licenseFile.LoadLicenseFile(); // This loads the file asynchronously 
                    EmbeddedLicense.DataContext = licenseFile;
                    EmbeddedLicenseHeader.DataContext = licenseFile;
                }
            }
        }

        private void OnDeclineButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void OnAcceptButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                DialogResult = true;
            }

            else if (e.Key == Key.D)
            {
                DialogResult = false;
            }
        }
    }
}