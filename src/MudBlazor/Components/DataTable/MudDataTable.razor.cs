using Microsoft.AspNetCore.Components;
using MudBlazor.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MudBlazor
{
    public partial class MudDataTable<T> : MudComponentBase
    {
        protected string Classname =>
       new CssBuilder("mud-datatable")
           //.AddClass($"mud-sm-table", Breakpoint == Breakpoint.Sm)
           //.AddClass($"mud-md-table", Breakpoint == Breakpoint.Md)
           //.AddClass($"mud-lg-table", Breakpoint == Breakpoint.Lg)
           //.AddClass($"mud-xl-table", Breakpoint == Breakpoint.Xl)
           .AddClass($"mud-datatable-dense", Dense)
         //.AddClass($"mud-table-hover", Hover)
         //.AddClass($"mud-table-outlined", Outlined)
         //.AddClass($"mud-table-square", Square)
         //.AddClass($"mud-table-sticky-header", FixedHeader)
         //.AddClass($"mud-elevation-{Elevation.ToString()}", !Outlined)
         .AddClass(Class)
       .Build();

        protected T Def
        {
            get
            {
                T t;
                T t1 = default(T);
                if (t1 == null)
                {
                    t = Activator.CreateInstance<T>();
                }
                else
                {
                    t1 = default(T);
                    t = t1;
                }
                return t;
            }
        }

        protected MudTable<T> mudTable;
        //protected ItemType Def => new ItemType();
        /// <summary>
        /// Text to show when there are no items to show
        /// </summary>
        [Parameter] public string EmptyDataText { get; set; }

        /// <summary>
        /// Gets or sets the data source that the DataTable is displaying data for.
        /// </summary>
        [Parameter]
        public IEnumerable<T> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                StateHasChanged();
            }
        }
        protected IEnumerable<T> _items { set; get; }
        /// <summary>
        /// Gets or sets a value indicating whether table footer is displayed.
        /// </summary>
        [Parameter]
        public bool ShowFooter { set; get; }
        /// <summary>
        /// Gets or sets a value indicating whether the pagination feature is enabled.
        /// </summary>
        [Parameter]
        public bool Pageing { set; get; } = false;
        /// <summary>
        /// Gets or sets a value indicating whether editing feature is enabled.
        /// </summary>
        [Parameter]
        public bool ReadOnly { set; get; }
        #region Parameters forwarded to internal MudTable

        /// <summary>
        /// Set true for rows with a narrow height
        /// </summary>
        [Parameter] public bool Dense { get; set; }

        /// <summary>
        /// Set true to see rows hover on mouse-over.
        /// </summary>
        [Parameter] public bool Hover { get; set; }
        /// <summary>
        /// At what breakpoint the table should switch to mobile layout. Takes Xs, Sm, Md, Lg and Xl the default behavior is breaking on Xs.
        /// </summary>
        [Parameter] public Breakpoint Breakpoint { get; set; } = Breakpoint.Xs;
        /// <summary>
        /// When true, the header will stay in place when the table is scrolled. Note: set Height to make the table scrollable.
        /// </summary>
        [Parameter] public bool FixedHeader { get; set; }

        /// <summary>
        /// Setting a height will allow to scroll the table. If not set, it will try to grow in height. You can set this to any CSS value that the
        /// attribute 'height' accepts, i.e. 500px. 
        /// </summary>
        [Parameter] public string Height { get; set; }
        /// <summary>
        /// If the table has more items than this number, it will break the rows into pages of said size.
        /// Note: requires a MudTablePager in PagerContent.
        /// </summary>
        [Parameter] public int RowsPerPage { get; set; } = 10;

        /// <summary>
        /// The page index of the currently displayed page (Zero based). Usually called by MudTablePager.
        /// Note: requires a MudTablePager in PagerContent.
        /// </summary>
        [Parameter] public int CurrentPage { get; set; }

        /// <summary>
        /// Set to true to enable selection of multiple rows with check boxes. 
        /// </summary>
        [Parameter] public bool MultiSelection { get; set; }

        /// <summary>
        /// Returns the item which was last clicked on in single selection mode (that is, if MultiSelection is false)
        /// </summary>
        [Parameter]
        public T SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (EqualityComparer<T>.Default.Equals(SelectedItem, value))
                    return;
                _selectedItem = value;
                SelectedItemChanged.InvokeAsync(value);
            }
        }
        private T _selectedItem;
        /// <summary>
        /// Callback is called when a row has been clicked and returns the selected item.
        /// </summary>
        [Parameter] public EventCallback<T> SelectedItemChanged { get; set; }

        /// <summary>
        /// If MultiSelection is true, this returns the currently selected items. You can bind this property and the initial content of the HashSet you bind it to will cause these rows to be selected initially.
        /// </summary>
        [Parameter] public HashSet<T> SelectedItems
        {
            get => _selectedItems;
            set
            {
                _selectedItems = value;
                SelectedItemsChanged.InvokeAsync(value);
            }
        }
        private HashSet<T>  _selectedItems;

        /// <summary>
        /// Callback is called whenever items are selected or deselected in multi selection mode.
        /// </summary>
        [Parameter] public EventCallback<HashSet<T>> SelectedItemsChanged { get; set; }
        /// <summary>
        /// Button click event.
        /// </summary>
        [Parameter] public EventCallback<TableRowClickEventArgs<T>> OnRowClick { get; set; } 
        
        /// <summary>
        /// Supply an async function which (re)loads filtered, paginated and sorted data from server.
        /// Table will await this func and update based on the returned TableData.
        /// Used only with ServerData
        /// </summary>
        [Parameter] public Func<TableState, Task<TableData<T>>> ServerData { get; set; }
        /// <summary>
        /// Call this to reload the server-filtered, -sorted and -paginated items
        /// </summary>
        public Task ReloadServerData()
        {
            return mudTable.ReloadServerData();
        }
        #endregion
        #region Templates
        [Parameter] public RenderFragment<T> Columns { get; set; }
        #endregion
    }
}
