function AppManager()
{

}

AppManager.Init = function ()
{
    
    vTabMgr.OnUpdate = AppManager.VTabClick;
    hTabMgr.OnUpdate = AppManager.RefreshMainView;

}

AppManager.VTabClick = function (arg)
{
    hTabMgr.ChangeKey(arg);
    hTabMgr.TabClick(null);
}

AppManager.RefreshMainView = function (arg) {
    $('#mainForm').submit();
}
