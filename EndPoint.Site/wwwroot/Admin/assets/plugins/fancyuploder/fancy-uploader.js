$(function () {
    //fancyfileuplod
    $('#demo').FancyFileUpload({
        params: {
            action: 'addPageImage'
        },
        maxfilesize: 1000000
    });
});