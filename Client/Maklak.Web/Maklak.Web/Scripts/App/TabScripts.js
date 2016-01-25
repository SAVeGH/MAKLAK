function TabManager(isVertical)
{
    var indexName = isVertical ? '#vSelectedValue' : '#hSelectedValue';
    var tabStripId = isVertical ? '#vTabStrip' : '#hTabStrip';

    
    this.OnUpdate;
    

    this.TabClick = function (tabId) {

        var currentSelection = $(indexName).val();

        if (tabId == currentSelection)
            return;
        
        $(indexName).val(tabId);

        $(tabStripId).submit();

    }

    this.OnComplete = function () {
        var form = $(tabStripId);

        $.validator.unobtrusive.parse(form);

        var completedTabId = $(indexName).val();

        this.OnUpdate(completedTabId);

    }
    

}


var hTabMgr = new TabManager(false);
var vTabMgr = new TabManager(true);



