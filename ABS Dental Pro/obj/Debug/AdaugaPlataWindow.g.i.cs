﻿#pragma checksum "..\..\AdaugaPlataWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4D516AF30AB35D6AEF5C9C2C5A4C03B03C408690B943D9BE868EDEB80009CD4B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ABS_Dental_Pro;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ABS_Dental_Pro {
    
    
    /// <summary>
    /// AdaugaPlataWindow
    /// </summary>
    public partial class AdaugaPlataWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 47 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbSituatie;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbMedic;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTotal;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTransa;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbRest;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbDescriere;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdaugaPlata;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\AdaugaPlataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker datePicker;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ABS Dental Pro;component/adaugaplatawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AdaugaPlataWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tbSituatie = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.cbMedic = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.tbTotal = ((System.Windows.Controls.TextBox)(target));
            
            #line 65 "..\..\AdaugaPlataWindow.xaml"
            this.tbTotal.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.NumericOnlyPreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 65 "..\..\AdaugaPlataWindow.xaml"
            this.tbTotal.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbTotal_KeyUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbTransa = ((System.Windows.Controls.TextBox)(target));
            
            #line 67 "..\..\AdaugaPlataWindow.xaml"
            this.tbTransa.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.NumericOnlyPreviewKeyDown);
            
            #line default
            #line hidden
            
            #line 67 "..\..\AdaugaPlataWindow.xaml"
            this.tbTransa.KeyUp += new System.Windows.Input.KeyEventHandler(this.tbTransa_KeyUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.tbRest = ((System.Windows.Controls.TextBlock)(target));
            
            #line 69 "..\..\AdaugaPlataWindow.xaml"
            this.tbRest.PreviewKeyDown += new System.Windows.Input.KeyEventHandler(this.NumericOnlyPreviewKeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tbDescriere = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnAdaugaPlata = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\AdaugaPlataWindow.xaml"
            this.btnAdaugaPlata.Click += new System.Windows.RoutedEventHandler(this.btnAdaugaPlata_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.datePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
