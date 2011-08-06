using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using AowEmailWrapper.Localization.Framework;
using AowEmailWrapper.Controls;
using AowEmailWrapper.Classes;

namespace AowEmailWrapper.Localization
{
    public class Translator
    {
        #region Private Members

        public const string DefaultLanguageCode = "en";
        private const string EnumLookupKeyTemplate = "enum{0}";
        private const string FormBlockPollingSetupEvery = "_every";
        private static Language _currentLanguage;
        private static Language _defaultLanguage;
        private static List<ComboBoxItem> _comboBoxItems;

        #endregion

        #region Private Methods

        private static Language GetLanguage(string code, Languages Languages)
        {
            return Languages.LanguageList.Find(lang => lang.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));;
        }

        private static Lookup GetLookup(string key)
        {
            Lookup returnVal = null;

            if (_currentLanguage != null)
            {
                Lookup theLookup = _currentLanguage.FindLookup(key);
                if (theLookup == null && _defaultLanguage != null)
                {
                    theLookup = _defaultLanguage.FindLookup(key);
                }
                returnVal = theLookup;
            }

            return returnVal;
        }

        private static void TranslateControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is ListView)
                {
                    ListView theListView = (ListView)control;
                    foreach (ColumnHeader col in theListView.Columns)
                    {
                        string key = string.Concat(theListView.Parent.Name, theListView.Name, col.Index);
                        col.Text = Translate(key);
                    }
                }
                else if (control is FormBlockCombo)
                {
                    FormBlockCombo theFormBlockCombo = (FormBlockCombo)control;
                    theFormBlockCombo.LabelName = Translate(theFormBlockCombo.Name);

                }
                else if (control is FormBlockText)
                {
                    FormBlockText theFormBlockText = (FormBlockText)control;
                    theFormBlockText.LabelName = Translate(theFormBlockText.Name);
                }
                else if (control is FormBlockCheckBox)
                {
                    FormBlockCheckBox theFormBlockCheckBox = (FormBlockCheckBox)control;
                    theFormBlockCheckBox.LabelName = Translate(theFormBlockCheckBox.Name);
                }
                else if (control is FormBlockPollingSetup)
                {
                    FormBlockPollingSetup theFormBlockPollingSetup = (FormBlockPollingSetup)control;
                    theFormBlockPollingSetup.CheckBoxText = Translate(theFormBlockPollingSetup.Name);
                    theFormBlockPollingSetup.EveryText = Translate(theFormBlockPollingSetup.Name + FormBlockPollingSetupEvery);
                }
                else
                {
                    if (!string.IsNullOrEmpty(control.Text))
                    {
                        string translated = Translate(control.Name);
                        if (!string.IsNullOrEmpty(translated))
                        {
                            control.Text = translated;
                        }
                    }
                    if (control.HasChildren)
                    {
                        TranslateControls(control.Controls);
                    }
                }
            }
        }

        #endregion

        #region Public Properties

        public static List<ComboBoxItem> ComboBoxItems
        {
            get { return _comboBoxItems; }
            set { _comboBoxItems = value; }
        }

        public static string CurrentLanguageCode
        {
            get { return (_currentLanguage != null) ? _currentLanguage.Code : null; }
        }

        #endregion

        #region Public Methods

        public static string Translate(string key)
        {
            Lookup theLookup = GetLookup(key);
            return (theLookup != null) ? theLookup.Value : string.Empty;
        }

        public static string Translate(string key, params string[] formatArgs)
        {
            Lookup theLookup = GetLookup(key);
            return (theLookup != null) ? string.Format(theLookup.Value, formatArgs) : string.Empty;
        }

        public static string TranslateEnum(object key)
        {
            Lookup theLookup = GetLookup(string.Format(EnumLookupKeyTemplate, key.ToString()));
            return (theLookup != null) ? theLookup.Value : string.Empty;
        }

        public static void TranslateForm(Form theForm)
        {
            TranslateControls(theForm.Controls);
            theForm.Text = Translate(theForm.Name);
        }

        public static string SetLanguage(string code, Languages Languages)
        {
            string returnVal = null;

            if (Languages != null &&
                 Languages.LanguageList != null &&
                 Languages.LanguageList.Count > 0)
            {
                _comboBoxItems = new List<ComboBoxItem>();
                Languages.LanguageList.ForEach(lang => _comboBoxItems.Add(new ComboBoxItem(lang.Code, lang.DisplayName)));

                _currentLanguage = GetLanguage(code, Languages);
                _defaultLanguage = GetLanguage(DefaultLanguageCode, Languages);

                if (_currentLanguage == null)
                {
                    _currentLanguage = _defaultLanguage;
                }

                returnVal = _currentLanguage.Code;
            }

            return returnVal;
        }

        #endregion
    }
}
