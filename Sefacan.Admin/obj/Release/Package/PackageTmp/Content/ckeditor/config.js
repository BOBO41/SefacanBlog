CKEDITOR.editorConfig = function (config) {
    config.filebrowserImageBrowseUrl = "/Content/ckeditor/ImageBrowser.aspx";
    config.filebrowserImageWindowWidth = 780;
    config.filebrowserImageWindowHeight = 720;
    config.filebrowserBrowseUrl = "/Content/ckeditor/LinkBrowser.aspx";
    config.filebrowserWindowWidth = 500;
    config.filebrowserWindowHeight = 650;
    config.removePlugins = 'basket,flashupload';
    config.language = 'tr';
    config.fullPage = true;
    config.allowedContent = true;
};