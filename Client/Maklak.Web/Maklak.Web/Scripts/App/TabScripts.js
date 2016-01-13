function TabManager(isVertical)
{
    var indexName = isVertical ? '#vSelectedValue' : '#hSelectedValue';
    var tabStripId = isVertical ? '#vTabStrip' : '#hTabStrip';

    
    this.OnUpdate;
    

    this.TabClick = function (tabId) {
        
        $(indexName).val(tabId);

        $(tabStripId).submit();

    }

    this.OnComplete = function () {
        var form = $(tabStripId);

        $.validator.unobtrusive.parse(form);

        this.OnUpdate(isVertical);

    }
    

}


var hTabMgr = new TabManager(false);
var vTabMgr = new TabManager(true);


function FF(arg) {
    alert(arg);
}

vTabMgr.OnUpdate = hTabMgr.TabClick;
hTabMgr.OnUpdate = FF;
