function AppManager()
{

}

AppManager.Init = function ()
{
    
    //vTabMgr.OnUpdate = AppManager.VTabClick;
    //hTabMgr.OnUpdate = AppManager.RefreshMainView;
}

AppManager.VTabClick = function (arg)
{
    hTabMgr.ChangeKey(arg);
    hTabMgr.TabClick(null);
}

AppManager.RefreshMainView = function (arg) {
    $('#mainForm').submit();
}

function GetURL() {

    var urlLocation = $(location).attr('href');

    var paramIndex = urlLocation.indexOf('?');
    // Обрезка параметров
    if (paramIndex > -1)
        urlLocation = urlLocation.substring(0, paramIndex);

    paramIndex = urlLocation.indexOf('#');

    if (paramIndex > -1)
        urlLocation = urlLocation.substring(0, paramIndex);

    //        if (urlLocation.charAt(urlLocation.length - 1) == "#")
    //            urlLocation = urlLocation.substring(0, urlLocation.length - 1);

    if (urlLocation.charAt(urlLocation.length - 1) == "/")
        urlLocation = urlLocation.substring(0, urlLocation.length - 1);

    return urlLocation;
}
