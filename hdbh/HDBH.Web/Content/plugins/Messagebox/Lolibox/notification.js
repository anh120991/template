//var Notification = {
//    Default: function (title, message) {
//        Lobibox.notify('default', {
//            size: 'mini',
//            title: title,
           
//            delayIndicator: false,
//            msg: message
//        });
//    },
//    Info: function (title, message) {
//        Lobibox.notify('info', {
//            size: 'mini',
//            title: title,

//            delayIndicator: false,
//            msg: message
//        });
//    },
//    Warning: function (title, message) {
//        Lobibox.notify('warning', {
//            size: 'mini',
//            title: title,
           
//            delayIndicator: false,
//            msg: message
//        });
//    },
//    Error: function (title, message) {

//        Lobibox.notify('error', {
//            size: 'mini',
//            title: title,
//            width: 0,
//            width: 400,
//            delayIndicator: false,
//            msg: message
//        });
//    },
//    Success: function (title, message) {
//        Lobibox.notify('success', {
//            size: 'mini',
//            title: title,
//            delayIndicator: true,
//            msg: message,
//            width: 0
//        });
//    }
//}

var Notification = {
    Default: function (title, message) {
        Lobibox.notify('default', {
            msg: message,
            closeOnClick: true
        });
    },
    Info: function (title, message) {
        Lobibox.alert('info', {
            msg: message,
            closeOnClick: true
        });
    },
    Warning: function (title, message) {
        Lobibox.alert('warning', {
            msg: message,
            closeOnClick: true
        });
    },
    Error: function (title, message) {

        Lobibox.alert('error', {
            msg: message,
            closeOnClick: true
        });
    },
    Success: function (title, message) {
        Lobibox.alert('success', {
            msg: message,
            closeOnClick: true
        });
    }
}

function ShowNotification(data) {
    var obj = JSON.parse(data);

}