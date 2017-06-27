function TabManager(isVertical)
{
    var indexName = isVertical ? '#vSelectedValue' : '#hSelectedValue';
    var tabStripId = isVertical ? '#vTabStrip' : '#hTabStrip';
    var keyName = isVertical ? '#vKey' : '#hKey';
    
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

    this.ChangeKey = function (newKey) {
        $(keyName).val(newKey);
    }
    

}


//var hTabMgr = new TabManager(false);
//var vTabMgr = new TabManager(true);


function TabMgr()
{
    this.TabClick = function (tabKey) {

        var element = $(event.target);        
        var form = element.closest('form');
        form.find("[name='Key']").val(tabKey);
        form.submit();
    }
}

var tabMgr = new TabMgr();


