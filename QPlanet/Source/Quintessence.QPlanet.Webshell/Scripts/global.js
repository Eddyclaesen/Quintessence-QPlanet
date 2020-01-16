(function ($) {
    $.fn.loading = function (message) {
        $(this).html('<p class="loading"><img src="/Images/loading_animation.gif" /><br />' + message + '</p>').fadeIn();
    };


    $.fn.showImage = function (img) {
        $(this).replaceWith('<img src="/Images/' + img + '" />').fadeIn();
    };

    $.getContrastColor = function (hexcolor) {
        var color = hexcolor.substring(1, 6);
        return (parseInt(color, 16) > 1048575 / 2) ? 'black' : 'white';
    };

    $.rgbToHex = function(rgb) {
        var rgbvals = /rgb\((.+),(.+),(.+)\)/i.exec(rgb);
        var rval = parseInt(rgbvals[1]);
        var gval = parseInt(rgbvals[2]);
        var bval = parseInt(rgbvals[3]);
        return '#' + (
            rval.toString(16) +
                gval.toString(16) +
                bval.toString(16)
        ).toUpperCase();
    };


    $.fn.showErrorMessage = function(message) {
        alert(message);
        $(this).html('<p class="loading"><img src="/Images/QPlanetError_128.png" /><br />An error has occured:<br />' + message + '<br/>Please contact your administrator if you saw this error before...</p>').fadeIn();
    };

    $.fn.tableRowToObject = function() {
        var arr = [];
        $(this).find('td').each(function () {
            var cellValue = $(this).html();
            arr.push(cellValue);
        });
        return arr;
    };

    $.alert = function(html, title, width, height) {
        var dialog = $('<div id="AlertDialog">'+html+'</div>');
        dialog.dialog({
            modal: true,
            title: title,
            width: width,
            height: height,
            buttons: [{ text: "Ok", click: function () { $(this).dialog("close"); } }]
        });
        return false;
    };
    
    $.fn.copyToListener = function (selector) {
        $(this).change(function () {
            var value = $(this).val();
            $(selector).val(value);
        });
    };

    $.fn.toIconLink = function (imageUrl, altText) {
        $(this).html('<img src="' + imageUrl + '" alt="' + altText + '"/>');
    };
    
    $.fn.toIconLink = function (imageUrl, altText, width, height) {
        $(this).html('<img src="' + imageUrl + '" alt="' + altText + '" style="width: ' + width + 'px; height: ' + height + 'px;"/>');
    };

    $.redirect = function (url) {
        window.location.href = url;
    };

    $.fn.parseDate = function (dateString) {
        if (dateString.length != 10)
            return null;

        try {
            var date = new Date();
            date.setDate(parseInt(dateString.substr(0, 2), 10));
            date.setMonth(parseInt(dateString.substr(3, 2), 10) - 1);
            date.setYear(parseInt(dateString.substr(5, 4), 10));

            return date;
        } catch (e) {
            alert(e);
            return null;
        } 
    };

    $.openOutlook = function(to, cc, bcc, subject, body) {
        try {
            var outlookApp = new ActiveXObject("Outlook.Application");
            var mailItem = outlookApp.CreateItem(0);
            mailItem.Subject = subject;
            mailItem.To = to;
            mailItem.CC = cc;
            mailItem.BCC = bcc;
            mailItem.HTMLBody = body;
            mailItem.display();
        }
        catch (e) {
            alert(e);
        }
    };
    
    /*
    Overload for openOutlook function with possibility to add attachments. 
    Attachments parameter is array of strings (contains url to file).
    */
    $.openOutlook = function (to, cc, bcc, subject, body, attachments) {
        try {
            var outlookApp = new ActiveXObject("Outlook.Application");
            var mailItem = outlookApp.CreateItem(0);
            mailItem.Subject = subject;
            mailItem.To = to;
            mailItem.CC = cc;
            mailItem.BCC = bcc;
            mailItem.HTMLBody = body;

            try {
                if (attachments != undefined && attachments != null && attachments.length != undefined && attachments.length != null) {
            for (var i = 0; i < attachments.length; i++) {
                mailItem.Attachments.add(attachments[i]);
            }
                }
            } catch (e) {
                alert('Unable to attach requested documents!');
            } 

            mailItem.display();
        }
        catch (e) {
            if (e == 'ReferenceError: ActiveXObject is not defined') {
                alert('Unable to start outlook from this browser. Please use Internet Explorer or install a plug-in for your browser.');
            } else {
                alert('Unable to start outlook: ' + e + ' Contact your system administrator. Maybe your security settings are to restricted for this site.');
            }
        }
    };
})(jQuery);