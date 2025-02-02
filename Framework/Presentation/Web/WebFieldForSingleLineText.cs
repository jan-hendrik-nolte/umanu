﻿/*********************************************************************
 * Umanu Framework / (C) Umanu Team / http://www.umanu.de/           *
 *                                                                   *
 * This program is free software: you can redistribute it and/or     *
 * modify it under the terms of the GNU Lesser General Public        *
 * License as published by the Free Software Foundation, either      *
 * version 3 of the License, or (at your option) any later version.  *
 *                                                                   *
 * This program is distributed in the hope that it will be useful,   *
 * but WITHOUT ANY WARRANTY; without even the implied warranty of    *
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the     *
 * GNU Lesser General Public License for more details.               *
 *                                                                   *
 * You should have received a copy of the GNU Lesser General Public  *
 * License along with this program.                                  *
 * If not, see <http://www.gnu.org/licenses/>.                       *
 *********************************************************************/

namespace Framework.Presentation.Web {

    using Framework.Presentation.Forms;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Field control for a single line of text.
    /// </summary>
    public class WebFieldForSingleLineText : WebFieldForElement {

        /// <summary>
        /// HTML type of input field.
        /// </summary>
        protected virtual string InputType {
            get {
                return "text";
            }
        }

        /// <summary>
        /// View field to build control for.
        /// </summary>
        public ViewFieldForSingleLineText ViewField { get; private set; }

        /// <summary>
        /// Instantiates a new instance.
        /// </summary>
        /// <param name="presentableField">presentable field to
        /// build control for</param>
        /// <param name="viewField">view field to build control for</param>
        /// <param name="renderMode">render mode of field, e.g. for
        /// form or for list table</param>
        /// <param name="topmostParentPresentableObject">topmost
        /// presentable parent object to build form for</param>
        /// <param name="optionDataProvider">data provider to use for
        /// option providers</param>
        /// <param name="comparisonDate">point in time to compare
        /// data of read-only fields to or null to not compare data</param>
        /// <param name="clientFieldIdPrefix">prefix to add to ID of
        /// field on client side</param>
        /// <param name="clientFieldIdSuffix">suffix to add to ID of
        /// field on client side</param>
        /// <param name="postBackState">post back state of the parent
        /// form</param>
        public WebFieldForSingleLineText(IPresentableFieldForElement presentableField, ViewFieldForSingleLineText viewField, FieldRenderMode renderMode, IPresentableObject topmostParentPresentableObject, IOptionDataProvider optionDataProvider, DateTime? comparisonDate, string clientFieldIdPrefix, string clientFieldIdSuffix, PostBackState postBackState)
            : base(presentableField, viewField, renderMode, topmostParentPresentableObject, optionDataProvider, comparisonDate, clientFieldIdPrefix, clientFieldIdSuffix, postBackState) {
            this.ViewField = viewField;
        }

        /// <summary>
        /// Renders a control for editing the value.
        /// </summary>
        /// <param name="html">HTML response</param>
        protected override void RenderEditableValue(HtmlWriter html) {
            var attributes = new Dictionary<string, string>(11);
            attributes.Add("id", this.ClientFieldId);
            attributes.Add("type", this.InputType);
            attributes.Add("name", this.ClientFieldId);
            if (this.ViewField.IsAutofocused) {
                attributes.Add("autofocus", "autofocus");
            }
            if (Mandatoriness.Required == this.ViewField.Mandatoriness) {
                attributes.Add("required", "required");
            }
            attributes.Add("maxlength", this.ViewField.MaxLength.ToString(CultureInfo.InvariantCulture));
            attributes.Add("minlength", this.ViewField.MinLength.ToString(CultureInfo.InvariantCulture));
            if (!string.IsNullOrEmpty(this.ViewField.OnClientKeyDown)) {
                attributes.Add("onKeyDown", this.ViewField.OnClientKeyDown);
            }
            attributes.Add("placeholder", System.Web.HttpUtility.HtmlEncode(this.ViewField.Placeholder));
            IDictionary<string, string> options;
            if (null == this.ViewField.OptionProvider) {
                options = new Dictionary<string, string>(0);
            } else {
                options = this.ViewField.OptionProvider.GetOptionDictionary(this.ParentPresentableObject, this.TopmostParentPresentableObject, this.OptionDataProvider);
            }
            if (options.Count > 0) {
                attributes.Add("list", "o" + this.ClientFieldId);
            }
            attributes.Add("pattern", System.Web.HttpUtility.HtmlEncode(this.ViewField.ValidationPattern));
            if (!string.IsNullOrEmpty(this.EditableValue)) {
                attributes.Add("value", System.Web.HttpUtility.HtmlEncode(this.EditableValue));
            }
            html.AppendSelfClosingTag("input", attributes);
            this.RenderDataList(html, "o" + this.ClientFieldId, options);
            return;
        }

    }

}
