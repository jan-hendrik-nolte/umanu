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

namespace Framework.BusinessApplications.Web.Controllers {

    using Framework.BusinessApplications.DataControllers;
    using Framework.Persistence;
    using Framework.Presentation;
    using Framework.Presentation.Forms;
    using Framework.Presentation.Web;
    using Presentation.Converters;
    using System;
    using System.Collections.Generic;
    using System.Web;

    /// <summary>
    /// HTTP controller for responding dynamic RSS files based on
    /// list table views.
    /// </summary>
    /// <typeparam name="T">type of providable objects</typeparam>
    public class RssListController<T> : ListFileController<T> where T : class, IProvidableObject {

        /// <summary>
        /// Delegate for resolval of link URL for an item.
        /// </summary>
        public OnClickUrlDelegate OnClickUrlDelegate { get; set; }

        /// <summary>
        /// Instantiates a new instance.
        /// </summary>
        /// <param name="businessApplication">business application to
        /// process</param>
        /// <param name="absoluteUrl">absolute URL of dynamic RSS
        /// file - it may not be empty, not contain any special
        /// charaters except for dashes and has to start with a slash</param>
        /// <param name="listTableDataController">data controller for
        /// dynamic RSS file</param>
        public RssListController(IBusinessApplication businessApplication, string absoluteUrl, ListTableDataController<T> listTableDataController)
            : base(businessApplication, absoluteUrl, listTableDataController) {
            this.OnClickUrlDelegate = delegate (IPresentableObject item) {
                return BusinessApplications.BusinessApplication.Url + "l/" + item.Id.ToString("N") + "/";
            };
        }

        /// <summary>
        /// Finds the file for a specific URL.
        /// </summary>
        /// <param name="url">URL of requested file</param>
        /// <returns>file for URL or null</returns>
        protected override File FindFile(Uri url) {
            File dynamicFile = null;
            if (url.AbsolutePath == this.AbsoluteUrl) {
                var listView = this.ListTableDataController.GetListTableView();
                if (null != listView) {
                    var fileName = url.Segments[url.Segments.LongLength - 1];
                    var optionDataProvider = new OptionDataProvider(this.BusinessApplication.PersistenceMechanism);
                    var queryResolver = new SubsetQueryResolver<T>(url, this.ListTableDataController.DataProvider, ulong.MaxValue);
                    var queryResult = queryResolver.Execute();
                    if (queryResult.ProvidableObjects.Count > 0) {
                        this.ListTableDataController.DataProvider.Preload(queryResult.ProvidableObjects, this.ListTableDataController.GetKeyChainsToPreloadForListTableView());
                    }
                    dynamicFile = this.GetFile(fileName, listView, queryResult.ProvidableObjects, optionDataProvider, queryResult.StartPosition, queryResult.MaxResults, queryResult.TotalCount);
                }
            }
            return dynamicFile;
        }

        /// <summary>
        /// Gets the dynamic file to be responded.
        /// </summary>
        /// <param name="fileName">name of file</param>
        /// <param name="listView">list view to apply to data in file</param>
        /// <param name="providableObjects">preloaded providable
        /// objects to write into file if property
        /// IsApplyingSkipAndTop is set to true, all matching
        /// providable objects non-preloaded otherwise</param>
        /// <param name="optionDataProvider">provider to use for
        /// querying additional data</param>
        /// <param name="skip">number of providable objects that were
        /// skiped</param>
        /// <param name="top">maximum number of providable objects
        /// they were limited to</param>
        /// <param name="total">total number of providable objects if
        /// skip and top would not have been applied</param>
        /// <returns>dynamic file to be responded</returns>
        protected virtual File GetFile(string fileName, IListTableView listView, ICollection<T> providableObjects, OptionDataProvider optionDataProvider, ulong skip, ulong top, ulong total) {
            this.ListTableDataController.DataProvider.Preload(providableObjects, this.ListTableDataController.GetKeyChainsToPreloadForListTableView());
            var rssWriter = new RssWriter(HttpContext.Current.Request.Url.AbsoluteUri, listView, providableObjects, optionDataProvider, this.OnClickUrlDelegate) {
                StartIndex = skip,
                ItemsPerPage = top,
                TotalResults = total
            };
            return rssWriter.WriteFile(fileName);
        }

    }

}