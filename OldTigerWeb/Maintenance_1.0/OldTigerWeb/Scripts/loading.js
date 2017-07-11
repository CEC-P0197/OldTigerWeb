// ローディング画面コントロール用スクリプト

var frmLoading;

// --------------------------------------------------
// 関数名   : OpenWindowLoading
// 概要     : ロード表示画面オープン
// 引数     : 無し     
// 戻り値   : 無し
// --------------------------------------------------
function openWindowLoading() {
    var urlLoading = './frmLoading.aspx';
    featuresLoading = 'menubar=no,toolbar=no,location=no,resizable=no,scrollbars=no,status=no,height=200,width=340,left=(window.screen.width-340)/2,top=(window.screen.height-200)/2';
    frmLoading = window.open(urlLoading, 'frmLoading', featuresLoading);
}

// --------------------------------------------------
// 関数名   : closeWindowLoading
// 概要     : ロード表示画面クローズ
// 引数     : 無し     
// 戻り値   : 無し
// --------------------------------------------------
function closeWindowLoading() {
    if (frmLoading != undefined) {
        frmLoading.close();
    }
}


