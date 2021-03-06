﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace BK20
{
    public partial class App : global::Windows.UI.Xaml.Markup.IXamlMetadataProvider
    {
    private global::BK20.BK20_XamlTypeInfo.XamlTypeInfoProvider _provider;

        /// <summary>
        /// GetXamlType(Type)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(global::System.Type type)
        {
            if(_provider == null)
            {
                _provider = new global::BK20.BK20_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByType(type);
        }

        /// <summary>
        /// GetXamlType(String)
        /// </summary>
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlType(string fullName)
        {
            if(_provider == null)
            {
                _provider = new global::BK20.BK20_XamlTypeInfo.XamlTypeInfoProvider();
            }
            return _provider.GetXamlTypeByName(fullName);
        }

        /// <summary>
        /// GetXmlnsDefinitions()
        /// </summary>
        public global::Windows.UI.Xaml.Markup.XmlnsDefinition[] GetXmlnsDefinitions()
        {
            return new global::Windows.UI.Xaml.Markup.XmlnsDefinition[0];
        }
    }
}

namespace BK20.BK20_XamlTypeInfo
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal partial class XamlTypeInfoProvider
    {
        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByType(global::System.Type type)
        {
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByType.TryGetValue(type, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByType(type);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlType GetXamlTypeByName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlType xamlType;
            if (_xamlTypeCacheByName.TryGetValue(typeName, out xamlType))
            {
                return xamlType;
            }
            int typeIndex = LookupTypeIndexByName(typeName);
            if(typeIndex != -1)
            {
                xamlType = CreateXamlType(typeIndex);
            }
            if (xamlType != null)
            {
                _xamlTypeCacheByName.Add(xamlType.FullName, xamlType);
                _xamlTypeCacheByType.Add(xamlType.UnderlyingType, xamlType);
            }
            return xamlType;
        }

        public global::Windows.UI.Xaml.Markup.IXamlMember GetMemberByLongName(string longMemberName)
        {
            if (string.IsNullOrEmpty(longMemberName))
            {
                return null;
            }
            global::Windows.UI.Xaml.Markup.IXamlMember xamlMember;
            if (_xamlMembers.TryGetValue(longMemberName, out xamlMember))
            {
                return xamlMember;
            }
            xamlMember = CreateXamlMember(longMemberName);
            if (xamlMember != null)
            {
                _xamlMembers.Add(longMemberName, xamlMember);
            }
            return xamlMember;
        }

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByName = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>
                _xamlTypeCacheByType = new global::System.Collections.Generic.Dictionary<global::System.Type, global::Windows.UI.Xaml.Markup.IXamlType>();

        global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>
                _xamlMembers = new global::System.Collections.Generic.Dictionary<string, global::Windows.UI.Xaml.Markup.IXamlMember>();

        string[] _typeNameTable = null;
        global::System.Type[] _typeTable = null;

        private void InitTypeTables()
        {
            _typeNameTable = new string[22];
            _typeNameTable[0] = "BK20.AboutPage";
            _typeNameTable[1] = "Windows.UI.Xaml.Controls.Page";
            _typeNameTable[2] = "Windows.UI.Xaml.Controls.UserControl";
            _typeNameTable[3] = "BK20.MessageShow";
            _typeNameTable[4] = "BK20.AVNightPage";
            _typeNameTable[5] = "BK20.BlankPage";
            _typeNameTable[6] = "BK20.ColumnPage";
            _typeNameTable[7] = "BK20.FavouritePage";
            _typeNameTable[8] = "BK20.InfoPage";
            _typeNameTable[9] = "BK20.LoginPage";
            _typeNameTable[10] = "AppArrange.WrapPanel.WrapPanel";
            _typeNameTable[11] = "Windows.UI.Xaml.Controls.Panel";
            _typeNameTable[12] = "Double";
            _typeNameTable[13] = "Windows.UI.Xaml.Controls.Orientation";
            _typeNameTable[14] = "BK20.MainPage";
            _typeNameTable[15] = "BK20.PlayerPage";
            _typeNameTable[16] = "BK20.RankPage";
            _typeNameTable[17] = "BK20.DataConverter1";
            _typeNameTable[18] = "Object";
            _typeNameTable[19] = "BK20.DataConverter2";
            _typeNameTable[20] = "BK20.ViewPage";
            _typeNameTable[21] = "BK20.WebPage";

            _typeTable = new global::System.Type[22];
            _typeTable[0] = typeof(global::BK20.AboutPage);
            _typeTable[1] = typeof(global::Windows.UI.Xaml.Controls.Page);
            _typeTable[2] = typeof(global::Windows.UI.Xaml.Controls.UserControl);
            _typeTable[3] = typeof(global::BK20.MessageShow);
            _typeTable[4] = typeof(global::BK20.AVNightPage);
            _typeTable[5] = typeof(global::BK20.BlankPage);
            _typeTable[6] = typeof(global::BK20.ColumnPage);
            _typeTable[7] = typeof(global::BK20.FavouritePage);
            _typeTable[8] = typeof(global::BK20.InfoPage);
            _typeTable[9] = typeof(global::BK20.LoginPage);
            _typeTable[10] = typeof(global::AppArrange.WrapPanel.WrapPanel);
            _typeTable[11] = typeof(global::Windows.UI.Xaml.Controls.Panel);
            _typeTable[12] = typeof(global::System.Double);
            _typeTable[13] = typeof(global::Windows.UI.Xaml.Controls.Orientation);
            _typeTable[14] = typeof(global::BK20.MainPage);
            _typeTable[15] = typeof(global::BK20.PlayerPage);
            _typeTable[16] = typeof(global::BK20.RankPage);
            _typeTable[17] = typeof(global::BK20.DataConverter1);
            _typeTable[18] = typeof(global::System.Object);
            _typeTable[19] = typeof(global::BK20.DataConverter2);
            _typeTable[20] = typeof(global::BK20.ViewPage);
            _typeTable[21] = typeof(global::BK20.WebPage);
        }

        private int LookupTypeIndexByName(string typeName)
        {
            if (_typeNameTable == null)
            {
                InitTypeTables();
            }
            for (int i=0; i<_typeNameTable.Length; i++)
            {
                if(0 == string.CompareOrdinal(_typeNameTable[i], typeName))
                {
                    return i;
                }
            }
            return -1;
        }

        private int LookupTypeIndexByType(global::System.Type type)
        {
            if (_typeTable == null)
            {
                InitTypeTables();
            }
            for(int i=0; i<_typeTable.Length; i++)
            {
                if(type == _typeTable[i])
                {
                    return i;
                }
            }
            return -1;
        }

        private object Activate_0_AboutPage() { return new global::BK20.AboutPage(); }
        private object Activate_3_MessageShow() { return new global::BK20.MessageShow(); }
        private object Activate_4_AVNightPage() { return new global::BK20.AVNightPage(); }
        private object Activate_5_BlankPage() { return new global::BK20.BlankPage(); }
        private object Activate_6_ColumnPage() { return new global::BK20.ColumnPage(); }
        private object Activate_7_FavouritePage() { return new global::BK20.FavouritePage(); }
        private object Activate_8_InfoPage() { return new global::BK20.InfoPage(); }
        private object Activate_9_LoginPage() { return new global::BK20.LoginPage(); }
        private object Activate_10_WrapPanel() { return new global::AppArrange.WrapPanel.WrapPanel(); }
        private object Activate_14_MainPage() { return new global::BK20.MainPage(); }
        private object Activate_15_PlayerPage() { return new global::BK20.PlayerPage(); }
        private object Activate_16_RankPage() { return new global::BK20.RankPage(); }
        private object Activate_17_DataConverter1() { return new global::BK20.DataConverter1(); }
        private object Activate_19_DataConverter2() { return new global::BK20.DataConverter2(); }
        private object Activate_20_ViewPage() { return new global::BK20.ViewPage(); }
        private object Activate_21_WebPage() { return new global::BK20.WebPage(); }

        private global::Windows.UI.Xaml.Markup.IXamlType CreateXamlType(int typeIndex)
        {
            global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType xamlType = null;
            global::BK20.BK20_XamlTypeInfo.XamlUserType userType;
            string typeName = _typeNameTable[typeIndex];
            global::System.Type type = _typeTable[typeIndex];

            switch (typeIndex)
            {

            case 0:   //  BK20.AboutPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_0_AboutPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 1:   //  Windows.UI.Xaml.Controls.Page
                xamlType = new global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 2:   //  Windows.UI.Xaml.Controls.UserControl
                xamlType = new global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 3:   //  BK20.MessageShow
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.UserControl"));
                userType.Activator = Activate_3_MessageShow;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 4:   //  BK20.AVNightPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_4_AVNightPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 5:   //  BK20.BlankPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_5_BlankPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 6:   //  BK20.ColumnPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_6_ColumnPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 7:   //  BK20.FavouritePage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_7_FavouritePage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 8:   //  BK20.InfoPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_8_InfoPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 9:   //  BK20.LoginPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_9_LoginPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 10:   //  AppArrange.WrapPanel.WrapPanel
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Panel"));
                userType.Activator = Activate_10_WrapPanel;
                userType.AddMemberName("ItemHeight");
                userType.AddMemberName("ItemWidth");
                userType.AddMemberName("Orientation");
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 11:   //  Windows.UI.Xaml.Controls.Panel
                xamlType = new global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 12:   //  Double
                xamlType = new global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 13:   //  Windows.UI.Xaml.Controls.Orientation
                xamlType = new global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 14:   //  BK20.MainPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_14_MainPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 15:   //  BK20.PlayerPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_15_PlayerPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 16:   //  BK20.RankPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_16_RankPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 17:   //  BK20.DataConverter1
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_17_DataConverter1;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 18:   //  Object
                xamlType = new global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType(typeName, type);
                break;

            case 19:   //  BK20.DataConverter2
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Object"));
                userType.Activator = Activate_19_DataConverter2;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 20:   //  BK20.ViewPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_20_ViewPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;

            case 21:   //  BK20.WebPage
                userType = new global::BK20.BK20_XamlTypeInfo.XamlUserType(this, typeName, type, GetXamlTypeByName("Windows.UI.Xaml.Controls.Page"));
                userType.Activator = Activate_21_WebPage;
                userType.SetIsLocalType();
                xamlType = userType;
                break;
            }
            return xamlType;
        }


        private object get_0_WrapPanel_ItemHeight(object instance)
        {
            var that = (global::AppArrange.WrapPanel.WrapPanel)instance;
            return that.ItemHeight;
        }
        private void set_0_WrapPanel_ItemHeight(object instance, object Value)
        {
            var that = (global::AppArrange.WrapPanel.WrapPanel)instance;
            that.ItemHeight = (global::System.Double)Value;
        }
        private object get_1_WrapPanel_ItemWidth(object instance)
        {
            var that = (global::AppArrange.WrapPanel.WrapPanel)instance;
            return that.ItemWidth;
        }
        private void set_1_WrapPanel_ItemWidth(object instance, object Value)
        {
            var that = (global::AppArrange.WrapPanel.WrapPanel)instance;
            that.ItemWidth = (global::System.Double)Value;
        }
        private object get_2_WrapPanel_Orientation(object instance)
        {
            var that = (global::AppArrange.WrapPanel.WrapPanel)instance;
            return that.Orientation;
        }
        private void set_2_WrapPanel_Orientation(object instance, object Value)
        {
            var that = (global::AppArrange.WrapPanel.WrapPanel)instance;
            that.Orientation = (global::Windows.UI.Xaml.Controls.Orientation)Value;
        }

        private global::Windows.UI.Xaml.Markup.IXamlMember CreateXamlMember(string longMemberName)
        {
            global::BK20.BK20_XamlTypeInfo.XamlMember xamlMember = null;
            global::BK20.BK20_XamlTypeInfo.XamlUserType userType;

            switch (longMemberName)
            {
            case "AppArrange.WrapPanel.WrapPanel.ItemHeight":
                userType = (global::BK20.BK20_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AppArrange.WrapPanel.WrapPanel");
                xamlMember = new global::BK20.BK20_XamlTypeInfo.XamlMember(this, "ItemHeight", "Double");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_0_WrapPanel_ItemHeight;
                xamlMember.Setter = set_0_WrapPanel_ItemHeight;
                break;
            case "AppArrange.WrapPanel.WrapPanel.ItemWidth":
                userType = (global::BK20.BK20_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AppArrange.WrapPanel.WrapPanel");
                xamlMember = new global::BK20.BK20_XamlTypeInfo.XamlMember(this, "ItemWidth", "Double");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_1_WrapPanel_ItemWidth;
                xamlMember.Setter = set_1_WrapPanel_ItemWidth;
                break;
            case "AppArrange.WrapPanel.WrapPanel.Orientation":
                userType = (global::BK20.BK20_XamlTypeInfo.XamlUserType)GetXamlTypeByName("AppArrange.WrapPanel.WrapPanel");
                xamlMember = new global::BK20.BK20_XamlTypeInfo.XamlMember(this, "Orientation", "Windows.UI.Xaml.Controls.Orientation");
                xamlMember.SetIsDependencyProperty();
                xamlMember.Getter = get_2_WrapPanel_Orientation;
                xamlMember.Setter = set_2_WrapPanel_Orientation;
                break;
            }
            return xamlMember;
        }
    }

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlSystemBaseType : global::Windows.UI.Xaml.Markup.IXamlType
    {
        string _fullName;
        global::System.Type _underlyingType;

        public XamlSystemBaseType(string fullName, global::System.Type underlyingType)
        {
            _fullName = fullName;
            _underlyingType = underlyingType;
        }

        public string FullName { get { return _fullName; } }

        public global::System.Type UnderlyingType
        {
            get
            {
                return _underlyingType;
            }
        }

        virtual public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name) { throw new global::System.NotImplementedException(); }
        virtual public bool IsArray { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsCollection { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsConstructible { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsDictionary { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsMarkupExtension { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsBindable { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsReturnTypeStub { get { throw new global::System.NotImplementedException(); } }
        virtual public bool IsLocalType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType ItemType { get { throw new global::System.NotImplementedException(); } }
        virtual public global::Windows.UI.Xaml.Markup.IXamlType KeyType { get { throw new global::System.NotImplementedException(); } }
        virtual public object ActivateInstance() { throw new global::System.NotImplementedException(); }
        virtual public void AddToMap(object instance, object key, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void AddToVector(object instance, object item)  { throw new global::System.NotImplementedException(); }
        virtual public void RunInitializer()   { throw new global::System.NotImplementedException(); }
        virtual public object CreateFromString(string input)   { throw new global::System.NotImplementedException(); }
    }
    
    internal delegate object Activator();
    internal delegate void AddToCollection(object instance, object item);
    internal delegate void AddToDictionary(object instance, object key, object item);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlUserType : global::BK20.BK20_XamlTypeInfo.XamlSystemBaseType
    {
        global::BK20.BK20_XamlTypeInfo.XamlTypeInfoProvider _provider;
        global::Windows.UI.Xaml.Markup.IXamlType _baseType;
        bool _isArray;
        bool _isMarkupExtension;
        bool _isBindable;
        bool _isReturnTypeStub;
        bool _isLocalType;

        string _contentPropertyName;
        string _itemTypeName;
        string _keyTypeName;
        global::System.Collections.Generic.Dictionary<string, string> _memberNames;
        global::System.Collections.Generic.Dictionary<string, object> _enumValues;

        public XamlUserType(global::BK20.BK20_XamlTypeInfo.XamlTypeInfoProvider provider, string fullName, global::System.Type fullType, global::Windows.UI.Xaml.Markup.IXamlType baseType)
            :base(fullName, fullType)
        {
            _provider = provider;
            _baseType = baseType;
        }

        // --- Interface methods ----

        override public global::Windows.UI.Xaml.Markup.IXamlType BaseType { get { return _baseType; } }
        override public bool IsArray { get { return _isArray; } }
        override public bool IsCollection { get { return (CollectionAdd != null); } }
        override public bool IsConstructible { get { return (Activator != null); } }
        override public bool IsDictionary { get { return (DictionaryAdd != null); } }
        override public bool IsMarkupExtension { get { return _isMarkupExtension; } }
        override public bool IsBindable { get { return _isBindable; } }
        override public bool IsReturnTypeStub { get { return _isReturnTypeStub; } }
        override public bool IsLocalType { get { return _isLocalType; } }

        override public global::Windows.UI.Xaml.Markup.IXamlMember ContentProperty
        {
            get { return _provider.GetMemberByLongName(_contentPropertyName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType ItemType
        {
            get { return _provider.GetXamlTypeByName(_itemTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlType KeyType
        {
            get { return _provider.GetXamlTypeByName(_keyTypeName); }
        }

        override public global::Windows.UI.Xaml.Markup.IXamlMember GetMember(string name)
        {
            if (_memberNames == null)
            {
                return null;
            }
            string longName;
            if (_memberNames.TryGetValue(name, out longName))
            {
                return _provider.GetMemberByLongName(longName);
            }
            return null;
        }

        override public object ActivateInstance()
        {
            return Activator(); 
        }

        override public void AddToMap(object instance, object key, object item) 
        {
            DictionaryAdd(instance, key, item);
        }

        override public void AddToVector(object instance, object item)
        {
            CollectionAdd(instance, item);
        }

        override public void RunInitializer() 
        {
            System.Runtime.CompilerServices.RuntimeHelpers.RunClassConstructor(UnderlyingType.TypeHandle);
        }

        override public object CreateFromString(string input)
        {
            if (_enumValues != null)
            {
                int value = 0;

                string[] valueParts = input.Split(',');

                foreach (string valuePart in valueParts) 
                {
                    object partValue;
                    int enumFieldValue = 0;
                    try
                    {
                        if (_enumValues.TryGetValue(valuePart.Trim(), out partValue))
                        {
                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                        }
                        else
                        {
                            try
                            {
                                enumFieldValue = global::System.Convert.ToInt32(valuePart.Trim());
                            }
                            catch( global::System.FormatException )
                            {
                                foreach( string key in _enumValues.Keys )
                                {
                                    if( string.Compare(valuePart.Trim(), key, global::System.StringComparison.OrdinalIgnoreCase) == 0 )
                                    {
                                        if( _enumValues.TryGetValue(key.Trim(), out partValue) )
                                        {
                                            enumFieldValue = global::System.Convert.ToInt32(partValue);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        value |= enumFieldValue; 
                    }
                    catch( global::System.FormatException )
                    {
                        throw new global::System.ArgumentException(input, FullName);
                    }
                }

                return value; 
            }
            throw new global::System.ArgumentException(input, FullName);
        }

        // --- End of Interface methods

        public Activator Activator { get; set; }
        public AddToCollection CollectionAdd { get; set; }
        public AddToDictionary DictionaryAdd { get; set; }

        public void SetContentPropertyName(string contentPropertyName)
        {
            _contentPropertyName = contentPropertyName;
        }

        public void SetIsArray()
        {
            _isArray = true; 
        }

        public void SetIsMarkupExtension()
        {
            _isMarkupExtension = true;
        }

        public void SetIsBindable()
        {
            _isBindable = true;
        }

        public void SetIsReturnTypeStub()
        {
            _isReturnTypeStub = true;
        }

        public void SetIsLocalType()
        {
            _isLocalType = true;
        }

        public void SetItemTypeName(string itemTypeName)
        {
            _itemTypeName = itemTypeName;
        }

        public void SetKeyTypeName(string keyTypeName)
        {
            _keyTypeName = keyTypeName;
        }

        public void AddMemberName(string shortName)
        {
            if(_memberNames == null)
            {
                _memberNames =  new global::System.Collections.Generic.Dictionary<string,string>();
            }
            _memberNames.Add(shortName, FullName + "." + shortName);
        }

        public void AddEnumValue(string name, object value)
        {
            if (_enumValues == null)
            {
                _enumValues = new global::System.Collections.Generic.Dictionary<string, object>();
            }
            _enumValues.Add(name, value);
        }
    }

    internal delegate object Getter(object instance);
    internal delegate void Setter(object instance, object value);

    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    internal class XamlMember : global::Windows.UI.Xaml.Markup.IXamlMember
    {
        global::BK20.BK20_XamlTypeInfo.XamlTypeInfoProvider _provider;
        string _name;
        bool _isAttachable;
        bool _isDependencyProperty;
        bool _isReadOnly;

        string _typeName;
        string _targetTypeName;

        public XamlMember(global::BK20.BK20_XamlTypeInfo.XamlTypeInfoProvider provider, string name, string typeName)
        {
            _name = name;
            _typeName = typeName;
            _provider = provider;
        }

        public string Name { get { return _name; } }

        public global::Windows.UI.Xaml.Markup.IXamlType Type
        {
            get { return _provider.GetXamlTypeByName(_typeName); }
        }

        public void SetTargetTypeName(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }
        public global::Windows.UI.Xaml.Markup.IXamlType TargetType
        {
            get { return _provider.GetXamlTypeByName(_targetTypeName); }
        }

        public void SetIsAttachable() { _isAttachable = true; }
        public bool IsAttachable { get { return _isAttachable; } }

        public void SetIsDependencyProperty() { _isDependencyProperty = true; }
        public bool IsDependencyProperty { get { return _isDependencyProperty; } }

        public void SetIsReadOnly() { _isReadOnly = true; }
        public bool IsReadOnly { get { return _isReadOnly; } }

        public Getter Getter { get; set; }
        public object GetValue(object instance)
        {
            if (Getter != null)
                return Getter(instance);
            else
                throw new global::System.InvalidOperationException("GetValue");
        }

        public Setter Setter { get; set; }
        public void SetValue(object instance, object value)
        {
            if (Setter != null)
                Setter(instance, value);
            else
                throw new global::System.InvalidOperationException("SetValue");
        }
    }
}

