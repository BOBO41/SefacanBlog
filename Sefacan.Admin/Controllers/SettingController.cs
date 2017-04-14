using Sefacan.Admin.Models;
using Sefacan.Core.Enums;
using Sefacan.Core.Helpers;
using Sefacan.Framework.Controllers;
using Sefacan.Framework.Helpers;
using Sefacan.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sefacan.Admin.Controllers
{
    public class SettingController : BaseController
    {
        #region Fields
        private readonly ISettingService settingService;
        #endregion

        #region Ctor
        public SettingController(ISettingService _settingService)
        {
            settingService = _settingService;
        }
        #endregion

        public ActionResult Index()
        {
            var model = new List<SettingModel>();
            IEnumerable<SettingType> settingCategories = Enum.GetValues(typeof(SettingType)).Cast<SettingType>();
            foreach (var item in settingCategories)
            {
                var setting = new SettingModel()
                {
                    Name = item.GetDisplayName(),
                    Type = item,
                    Settings = settingService.GetSettings(item).Select(x => new SettingItemModel
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Value = x.Value,
                        SelectedValue = x.SelectedValue,
                        Type = x.InputType
                    }).ToList()
                };
                model.Add(setting);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string[] category, string[] settingTextbox, string[] settingCheckbox,
            string[] settingDatebox, string[] settingTimebox, string[] settingEditor, string[] settingSelect)
        {
            if (category != null && category.Length > 0)
            {
                foreach (var item in category)
                {
                    var setting = item.ToEnum<SettingType>();
                    var textboxSetting = settingService.GetSetting(setting, Input.TextBox);
                    var checkboxSetting = settingService.GetSetting(setting, Input.CheckBox);
                    var dateboxSetting = settingService.GetSetting(setting, Input.Date);
                    var timeboxSetting = settingService.GetSetting(setting, Input.Time);
                    var editorSetting = settingService.GetSetting(setting, Input.Editor);
                    var selectSetting = settingService.GetSetting(setting, Input.Select);

                    if (settingTextbox != null && settingTextbox.Length > 0)
                    {
                        int i = 0;
                        foreach (var textbox in textboxSetting)
                        {
                            if (textbox.Value != settingTextbox[i])
                            {
                                textbox.Value = settingTextbox[i];
                                settingService.UpdateSetting(textbox);
                            }
                            i++;
                        }
                    }

                    if (settingCheckbox != null && settingCheckbox.Length > 0)
                    {
                        int i = 0;
                        foreach (var checkbox in checkboxSetting)
                        {
                            checkbox.Value = settingCheckbox[i] == "on" ? "true" : "false";
                            settingService.UpdateSetting(checkbox);
                            i++;
                        }
                    }

                    if (settingDatebox != null && settingDatebox.Length > 0)
                    {
                        int i = 0;
                        foreach (var datebox in dateboxSetting)
                        {
                            if (datebox.Value != settingDatebox[i])
                            {
                                datebox.Value = settingDatebox[i];
                                settingService.UpdateSetting(datebox);
                            }
                            i++;
                        }
                    }

                    if (settingTimebox != null && settingTimebox.Length > 0)
                    {
                        int i = 0;
                        foreach (var timebox in timeboxSetting)
                        {
                            if (timebox.Value != settingTimebox[i])
                            {
                                timebox.Value = settingTimebox[i].ToLower().Replace("am", string.Empty).Replace("pm", string.Empty).Trim();
                                settingService.UpdateSetting(timebox);
                            }
                            i++;
                        }
                    }

                    if (settingEditor != null && settingEditor.Length > 0)
                    {
                        int i = 0;
                        foreach (var editor in editorSetting)
                        {
                            if (editor.Value != settingEditor[i])
                            {
                                editor.Value = settingEditor[i];
                                settingService.UpdateSetting(editor);
                            }
                            i++;
                        }
                    }

                    if (settingSelect != null && settingSelect.Length > 0)
                    {
                        int i = 0;
                        foreach (var selectbox in selectSetting)
                        {
                            if (selectbox.SelectedValue != settingSelect[i])
                            {
                                selectbox.SelectedValue = settingSelect[i];
                                settingService.UpdateSetting(selectbox);
                            }
                            i++;
                        }
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}