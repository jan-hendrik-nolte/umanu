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

namespace Framework.BusinessApplications.DataProviders {

    using Framework.BusinessApplications.Workflows;
    using Framework.BusinessApplications.Workflows.Steps;
    using Framework.Persistence;
    using Framework.Persistence.Filters;
    using Framework.Presentation;
    using System.Collections.Generic;

    /// <summary>
    /// Data provider for form actions.
    /// </summary>
    internal sealed class FormActionDataProvider : DataProvider<IProvidableObject> {

        /// <summary>
        /// Associated workflow controlled object.
        /// </summary>
        public WorkflowControlledObject AssociatedObject { get; private set; }

        /// <summary>
        /// Associated form action.
        /// </summary>
        public FormAction FormAction { get; private set; }

        /// <summary>
        /// Instantiates a new instance.
        /// </summary>
        /// <param name="associatedObject">associated workflow
        /// controlled object</param>
        /// <param name="formAction">associated form action</param>
        public FormActionDataProvider(WorkflowControlledObject associatedObject, FormAction formAction)
            : base() {
            this.AssociatedObject = associatedObject;
            this.FormAction = formAction;
        }

        /// <summary>
        /// Adds or updates an object in persistence mechanism.
        /// </summary>
        /// <param name="element">object to add or update</param>
        public override void AddOrUpdate(IProvidableObject element) {
            var elevatedPersistenceMechanism = this.AssociatedObject.ParentPersistentContainer.ParentPersistenceMechanism.CopyWithElevatedPrivileges();
            var elevatedFormAction = elevatedPersistenceMechanism.FindContainer<WorkflowStep>().FindOne(this.FormAction.Id) as FormAction;
            elevatedFormAction.UpdatedFormData = element;
            this.AssociatedObject.WorkflowWithElevatedPrivileges.Execute();
            return;
        }

        /// <summary>
        /// Creates a new object of a specific type.
        /// </summary>
        /// <param name="type">type of new object</param>
        /// <returns>new object of specified type or null if current
        /// user is not allowed to create objects</returns>
        public override IProvidableObject Create(System.Type type) {
            return null;
        }

        /// <summary>
        /// Deletes an object in persistence mechanism.
        /// </summary>
        /// <param name="element">object to delete</param>
        public override void Delete(IProvidableObject element) {
            //  nothing to do
            return;
        }

        /// <summary>
        /// Finds all matching results of this data provider.
        /// </summary>
        /// <param name="filterCriteria">filter criteria to select
        /// objects for</param>
        /// <param name="sortCriteria">criteria to sort objects by</param>
        /// <returns>all matching objects from this container</returns>
        /// <param name="startPosition">index of first position in
        /// results to return - "0" is the lowest index: "0" would
        /// return all results, whereas "5" would skip the five first
        /// results (this is useful for paging)</param>
        /// <param name="maxResults">maximum number of results to
        /// return</param>
        /// <returns>matching objects from this data provider</returns>
        public override ICollection<IProvidableObject> Find(FilterCriteria filterCriteria, SortCriterionCollection sortCriteria, ulong startPosition, ulong maxResults) {
            return new IProvidableObject[0];
        }

        /// <summary>
        /// Finds the object with a specific ID.
        /// </summary>
        /// <param name="id">ID to get providable object for</param>
        /// <returns>object with specific ID or null if no
        /// match was found</returns>
        public override IProvidableObject FindOne(string id) {
            return null;
        }

        /// <summary>
        /// Finds the file with a specific ID and file name.
        /// </summary>
        /// <param name="fileId">ID to get file for</param>
        /// <param name="fileName">name of requested file</param>
        /// <returns>file with specific ID and file name</returns>
        public override File FindFile(System.Guid fileId, string fileName) {
            return null;
        }

        /// <summary>
        /// Preloads the state of multiple objects.
        /// </summary>
        /// <param name="elements">objects to preload</param>
        /// <param name="keyChains">key chains of properties to be
        /// preloaded</param>
        public override void Preload(IEnumerable<IProvidableObject> elements, IEnumerable<string[]> keyChains) {
            //  nothing to do
            return;
        }

    }

}