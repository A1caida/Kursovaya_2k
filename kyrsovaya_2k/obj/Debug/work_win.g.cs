﻿#pragma checksum "..\..\work_win.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8A32679DD9238C8350D17CA377F2295EEFB568624A3CBFD79F9E57FB88D007CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using kyrsovaya_2k;


namespace kyrsovaya_2k {
    
    
    /// <summary>
    /// work_win
    /// </summary>
    public partial class work_win : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock name;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button s_auth;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid authors;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox authsur;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox authname;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox authpatr;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox authyear;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\work_win.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button add_auth;
        
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
            System.Uri resourceLocater = new System.Uri("/kyrsovaya_2k;component/work_win.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\work_win.xaml"
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
            
            #line 11 "..\..\work_win.xaml"
            ((System.Windows.Controls.TabControl)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.TabControl_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.s_auth = ((System.Windows.Controls.Button)(target));
            
            #line 34 "..\..\work_win.xaml"
            this.s_auth.Click += new System.Windows.RoutedEventHandler(this.search_auth);
            
            #line default
            #line hidden
            return;
            case 4:
            this.authors = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.authsur = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.authname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.authpatr = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.authyear = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.add_auth = ((System.Windows.Controls.Button)(target));
            
            #line 56 "..\..\work_win.xaml"
            this.add_auth.Click += new System.Windows.RoutedEventHandler(this.add_authh);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

