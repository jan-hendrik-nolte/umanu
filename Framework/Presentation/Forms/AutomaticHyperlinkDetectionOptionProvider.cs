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

namespace Framework.Presentation.Forms {

    using Framework.Presentation;
    using Framework.Properties;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Provider for automatic hyperlink detection options.
    /// </summary>
    public class AutomaticHyperlinkDetectionOptionProvider : CachedOptionProvider {

        /// <summary>
        /// Instantiates a new instance.
        /// </summary>
        public AutomaticHyperlinkDetectionOptionProvider()
            : base() {
            // nothing to do
        }

        /// <summary>
        /// Gets all options of this option provider.
        /// </summary>
        /// <param name="optionDataProvider">data provider to use for
        /// option providers</param>
        /// <returns>all options of this option provider</returns>
        public override IEnumerable<KeyValuePair<string, string>> GetOptions(IOptionDataProvider optionDataProvider) {
            yield return new KeyValuePair<string, string>(((int)AutomaticHyperlinkDetection.IsEnabled).ToString(CultureInfo.InvariantCulture), Resources.Yes);
            yield return new KeyValuePair<string, string>(((int)AutomaticHyperlinkDetection.IsDisabled).ToString(CultureInfo.InvariantCulture), Resources.No);
        }

    }

}