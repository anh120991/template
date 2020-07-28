var ItemPagePage = itemperpage;
jQuery.fn.pagination = function (maxentries, opts) {
    opts = jQuery.extend({
        items_per_page: ItemPagePage,
        num_display_entries:5,
        current_page: 0,
        num_edge_entries:2,
        link_to: "",
        prev_text: '<i class="fa fa-angle-double-left" aria-hidden="true"></i>',
        next_text: '<i class="fa fa-angle-double-right" aria-hidden="true"></i>',
         ellipse_text: "...",
        resultdata: null,
        elementdatabinding: "",
        prev_show_always: true,
        next_show_always: true,  
        callback: function () {
            console.log('callback');
            return false;
        }
    }, opts || {});
     
    return this.each(function () {
        /**
		 * Calculate the maximum number of pages
		 */
        function numPages() {
            return Math.ceil(maxentries / itemperpage);
        }


        /**
		 * Show Pagin Function
		 */
        //function ShowPaging(page_id) {
            
        //}


        /**
		 * Calculate start and end point of pagination links depending on 
		 * current_page and num_display_entries.
		 * @return {Array}
		 */
        function getInterval() {
            var ne_half = Math.ceil(opts.num_display_entries / 2);
            var np = numPages();
            var upper_limit = np - opts.num_display_entries;
            var start = current_page > ne_half ? Math.max(Math.min(current_page - ne_half, upper_limit), 0) : 0;
            var end = current_page > ne_half ? Math.min(current_page + ne_half, np) : Math.min(opts.num_display_entries, np);
            return [start, end];
        }

        /**
		 * This is the event handling function for the pagination links. 
		 * @param {int} page_id The new page number
		 */
        function pageSelected(page_id, evt) {
            
            current_page = page_id;
            drawLinks();
            if (opts.resultdata != null) {
                var continuePropagation;
                if (opts.elementdatabinding == "searchUseList") {
                    continuePropagation = ShowPaging(page_id, $("#searchUseList"));

                }
                else if (opts.elementdatabinding == "searchUseListRF") {
                    {
                        continuePropagation = ShowPagingRF(page_id, $("#searchUseListRF"));
                    }
                }

            }
            else {
                
                continuePropagation = opts.callback(page_id, panel);

            }
            if (!continuePropagation) {
                if (evt.stopPropagation) {
                    evt.stopPropagation();
                }
                else {
                    evt.cancelBubble = true;
                }
            }

            return continuePropagation;
        }
        /**
		 * This function inserts the pagination links into the container element
		 */
        function drawLinks() {
            panel.empty();
            var interval = getInterval();
            var np = numPages();
            // This helper function returns a handler function that calls pageSelected with the right page_id
            var getClickHandler = function (page_id) {
                return function (evt) { return pageSelected(page_id, evt); }
            }
            // Helper function for generating a single link (or a span tag if it's the current page)
            var appendItem = function (page_id, appendopts) {
                page_id = page_id < 0 ? 0 : (page_id < np ? page_id : np - 1); // Normalize page id to sane value
                appendopts = jQuery.extend({ text: page_id + 1, classes: "" }, appendopts || {});
                if (page_id == current_page) {
                    var lnk = jQuery("<li class='pageNumber active'><a>" + (appendopts.text) + "</a></li>");
                }
                else {
                    var lnk = jQuery("<li><a>" + (appendopts.text) + "</a></li>")
						.bind("click", getClickHandler(page_id))						
                    ;


                }
                if (appendopts.classes) { lnk.addClass(appendopts.classes); }
                panel.append(lnk);
            }
            // Generate "Previous"-Link
            if (opts.prev_text && (current_page > 0 || opts.prev_show_always)) {
                appendItem(current_page - 1, { text: opts.prev_text, classes: "firstPage" });
            }
            // Generate starting points
            if (interval[0] > 0 && opts.num_edge_entries > 0) {
                var end = Math.min(opts.num_edge_entries, interval[0]);
                for (var i = 0; i < end; i++) {
                    appendItem(i);
                }
                if (opts.num_edge_entries < interval[0] && opts.ellipse_text) {
                    jQuery("<li>" + opts.ellipse_text + "</li>").appendTo(panel);
                }
            }
            // Generate interval links
            for (var i = interval[0]; i < interval[1]; i++) {
                appendItem(i);
            }
            // Generate ending points
            if (interval[1] < np && opts.num_edge_entries > 0) {
                if (np - opts.num_edge_entries > interval[1] && opts.ellipse_text) {
                    jQuery("<li>" + opts.ellipse_text + "</li>").appendTo(panel);
                }
                var begin = Math.max(np - opts.num_edge_entries, interval[1]);
                for (var i = begin; i < np; i++) {
                    appendItem(i);
                }

            }
            // Generate "Next"-Link
            if (opts.next_text && (current_page < np - 1 || opts.next_show_always)) {
                appendItem(current_page + 1, { text: opts.next_text, classes: "lastPage" });
            }
        }

        // Extract current_page from options
        var current_page = opts.current_page;
        // Create a sane value for maxentries and items_per_page
        maxentries = (!maxentries || maxentries < 0) ? 1 : maxentries;
        opts.items_per_page = (!opts.items_per_page || opts.items_per_page < 0) ? 1 : opts.items_per_page;
        // Store DOM element for easy access from all inner functions
        var panel = jQuery(this);
        // Attach control functions to the DOM element 
        this.selectPage = function (page_id) { pageSelected(page_id); }
        this.prevPage = function () {
            if (current_page > 0) {
                pageSelected(current_page - 1);
                return true;
            }
            else {
                return false;
            }
        }
        this.nextPage = function () {
            if (current_page < numPages() - 1) {
                
                pageSelected(current_page + 1);
                return true;
            }
            else {
                return false;
            }
        }
        // When all initialisation is done, draw the links
        // When all initialisation is done, draw the links
        console.log(maxentries);
        console.log(opts.items_per_page);
        console.log(parseInt(maxentries) > parseInt(opts.items_per_page));
        if (parseInt(maxentries) > parseInt(opts.items_per_page)) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
        drawLinks();
        // call callback function
        opts.callback(current_page, this);
    });
}
