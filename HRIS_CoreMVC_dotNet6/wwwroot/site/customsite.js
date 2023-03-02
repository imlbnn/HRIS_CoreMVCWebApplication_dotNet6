//app loading
var appLoading = {
    suspended: false,
    show: function () {
        $("#site-loading").show();
    },
    hide: function () {
        $("#site-loading").fadeOut(150);
    }
};

function DisplayErrorTab(){ 
    //remove the tab-header error class
    $('#myTab li a').removeClass("tab-header-error");
    //iterate all tabheader to add class tab-header-error
    $('#myTab li').each(function (index) {
        //get the a tag 
        let tabContentId = $(this).find('a').data("tab-content-id");
        let $activeTab = $("#myTab li a").hasClass("active");
        //check if tabContent has an error
        if (($("#" + tabContentId).find('.requiredFieldError').length !== 0) ||
            ($("#" + tabContentId).find('.field-validation-error').length !== 0)){
            $(this).find('a').addClass("tab-header-error");
        } 
    });


    //iterate all tabheader to activate the nearest with error
    $('#myTab li').each(function (index) {
        //get the a tag 
        let tabContentId = $(this).find('a').data("tab-content-id");
        let $activeTab = $("#myTab li a.active");
        let tabContentActiveId = $activeTab.data("tab-content-id"); 
        let $activeTabContent = $("#myTab li a.active");
        //check if tabContent has an error
        if (($("#" + tabContentId).find('.requiredFieldError').length !== 0) ||
            ($("#" + tabContentId).find('.field-validation-error').length !== 0)) {

            $activeTab.removeClass("active show");
            $("#" + tabContentActiveId).removeClass("active show");
            $(this).find('a').addClass("active show");
            $("#" + tabContentId).addClass("active show");
            $("#" + tabContentId).scrollTop(1000);
            return false;
        }
    }); 
}

function ToggleMenuBox(){ 
    //if($('#topNavbar').hasClass('responsive')){
    //    $('#topNavbar').removeClass('responsive');
    //}
    //else{
    //    $('#topNavbar').addClass('responsive');
    //}
    $('#contentContainer').toggleClass('showMenu');
    $('#sidenav').toggleClass('showMenu');
    $('#bluredDiv').toggleClass('showMenu');
}

var appMessage = {
    success: function (message) {
        var $message = $("#message");
        $message.html('<div class="alert alert-success" role="alert">' + message + '</div>');
        $message.show();
    },
    fail: function (message) {
        var $message = $("#message");
        $message.html('<div class="alert alert-warning" role="alert">' + message + '</div>');
        $message.show();
    }
};

//app modal :: do not forget to assign modal-content width
var appModal = {  
    show: function (modalId, callBack) {
        var $modal = $("#" + modalId);
        var $content = $modal.find(".c-modal-content");
        var $mWidth = $content.css("width");
        $modal.show(0, function () {
            $content.addClass("show");
            if (callBack != undefined || callBack != null) callBack();
        });
    },
    close: function (modalId, delay) {
        var $modal = $("#" + modalId);
        var $content = $modal.find(".c-modal-content");
        var $mWidth = $content.css("width");
        if (delay != null) {
            setTimeout(function () {
                process();
            }, delay);
        }
        else {
            process();
        }
        function process() {
            $content.removeClass("show");
            setTimeout(function () { $modal.hide(); }, 80);
        }
    }
};

var appMoreModal = {  
    show: function (modalId, callBack) {
        var $modal = $("#" + modalId);
        var $content = $modal.find(".m-modal-content"); 
        $content.addClass("showMore");
        $modal.addClass("showMore");
        $content.on('click',function(){
            appMoreModal.close(modalId);
        });

        //$modal.show(0, function () {
        //    $content.addClass("showMore");
        //    $modal.addClass("showMore");
        //    $content.on('click',function(){
        //        appMoreModal.close(modalId);
        //    });
        //    if (callBack != undefined || callBack != null) callBack();
        //});
    },
    close: function (modalId, delay) {
        var $modal = $("#" + modalId);
        var $content = $modal.find(".m-modal-content");
        //var $mWidth = $content.css("width");
        if (delay != null) {
            setTimeout(function () {
                process();
            }, delay);
        }
        else {
            process();
        }
        function process() {
            $content.removeClass("showMore");
            $modal.removeClass("showMore");
            setTimeout(function () { $modal.hide(); }, 80);
        }
    }
};

//image viewer
function imageViewer(image) {
    var viewer = new Viewer(image, {
        inline: false,
        viewed: function () {
            viewer.zoomTo(1);
        }
    });
}

//ajax method
var ajaxMethod = {
    GET: "GET",
    POST: "POST"
};

//app page operation
var pageModes = {
    add: 1,
    update: 2
}
var pageMode = null;

//on document ready
$(function () {
    //unobtrusive validation
    $.fn.clearUnobtrusiveValidationErrors = function () {
        $(this).each(function () {
            $(this).find(".field-validation-error").empty();
            $(this).trigger('reset.unobtrusiveValidation');
        });
    };

    //misc
    $.fn.readOnly = function () {
        return this.each(function () {
            $(this).attr("readonly", "readonly");
        });
    }
    $.fn.disable = function () {
        return this.each(function () {
            $(this).attr("disabled", "disabled");
        });
    }
    $.fn.enable = function () {
        return this.each(function () {
            $(this).removeAttr("disabled");
        });
    }
    $.fn.mode = function (mode) {
        return $(this).attr("data-mode", mode);
    }
    $.fn.textBindToTableData = function (tableInstance) {
        return $(this)
                    .on("focusout", function () {
                        var $this = $(this);
                        var $tr = $this.closest("tr");
                        var $table = tableInstance;
                        var data = $table.row($tr).data();
                        data[$this.attr("name")] = $this.val();
                    });
    }
    $.fn.textChangeBindToTableData = function (tableInstance) {
        return $(this)
            .on("change", function () {
                var $this = $(this);
                var $tr = $this.closest("tr");
                var $table = tableInstance;
                var data = $table.row($tr).data();
                data[$this.attr("name")] = $this.val();
            });
    }

    $.fn.checkBoxBindToTableData = function (tableInstance) {
        return $(this)
            .on("change", function () {
                var $this = $(this);
                var $tr = $this.closest("tr");
                var $table = tableInstance;
                var data = $table.row($tr).data();
                data[$this.attr("name")] = $this.is(":checked");
            });
    }

    //fields rules
    $.fn.forceNumeric = function () {
        return this.each(function () {
            $(this).keypress(function (e) {
                var charCode = (e.which) ? e.which : event.keyCode
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;

                return true;
            });
        });
    }
    $.fn.forceDecimalPlaces = function (decimalPlaces) {
        return this.each(function () {
            $(this).keypress(function (evt) {
                var charCode = (evt.which) ? evt.which : event.keyCode;
                var number = $(this).val().split('.');
                if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                    return false;
                }
                //just one dot
                if (number.length > 1 && charCode == 46) {
                    return false;
                }
                //get the carat position
                var caratPos = getSelectionStart(this);
                var dotPos = $(this).val().indexOf(".");
                if (caratPos > dotPos && dotPos > -1 && (number[1].length > (decimalPlaces-1))) {
                    return false;
                }
                return true;
            });
        });
    }

    //accordion menu
    $(".sn a").click(function (e) {
        var $menu = $(this);
        if ($menu.attr("href") == "#") {
            e.preventDefault();
        }
        var $menuChild = $menu.next();
        if (!$menuChild.is(":visible")) {
            $menuChild.show();
            if ($menu.hasClass("menu-parent")) {
                $(".sn-active").click();
                $menu.addClass("sn-active");
                $menu.find("i").removeClass("ion-ios-arrow-forward");
                $menu.find("i").addClass("ion-ios-arrow-down");
            }
        }
        else {
            $menuChild.hide();
            if ($menu.hasClass("menu-parent")) {
                $menu.removeClass("sn-active");
                $menu.find("i").addClass("ion-ios-arrow-forward");
                $menu.find("i").removeClass("ion-ios-arrow-down");
            }
        }
    });

    //open menu on page load
    var $active = $("a[href='" + window.location.pathname.toLocaleLowerCase() + "']");
    $active.each(function () { this.style.setProperty('color', 'rgb(68, 30, 121)', 'important') });
    openMenu($active);

    //app modal initialized
    $(".c-modal").click(function (e) {
        if (e.target !== this) return;
        var modal = $(this).attr("id");
        appModal.close(modal);
    });
    $(".c-modal-close").click(function () {
        var modal = $(this).closest(".c-modal").attr("id");
        appModal.close(modal);
    });
    $(document).find("[data-modal='true']").click(function () {
        appModal.show($(this).attr("data-target-modal-id"));
    });

    //app loading function
    //$(document).ajaxStart(function () {
    //  if (!appLoading.suspended) appLoading.show();
    //});
    //$(document).ajaxStop(function () {        
    //  if (!appLoading.suspended) setTimeout(function () { appLoading.hide(); }, 100);//add delay for 100ms
    //}); 

    //upload
    $(document).on("change", ".upload-field",function () {
        var formData = new FormData()
        var allowedFileType = ["jpg","jpeg","png"];
        var allowedMBSize = 5;
        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var fileName = "";

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileName = files[i].name;
                if(!allowedFileType.includes(files[i].name.split(".")[1].toLowerCase()) )
                {
                    alert("File must be image only.");
                    return;
                }
                var fileSize = parseInt(files[i].size / 1024);
                var fileSized = "KB";
                if (fileSize > 1024) {
                    fileSize = parseInt(fileSize / 1024);
                    if(fileSize >= allowedMBSize)
                    {
                        //alert("Max file size must not exceed "+allowedMBSize +"MB.");
                        alert(allowedMBSize +"MB file size limit.");
                        return;
                    }
                    fileSized = "MB";
                }
            }
            for (var i = 0; i < files.length; i++) {
                formData.append(files[i].name, files[i]);
                fileName = files[i].name;
                

                $("#con-files").append('<tr style="display:none">' +
                    '<td><img class="img-view" width="100px" height="80px" onclick="imageViewer(this)" style="border: 1px solid gray;" /></td>' +
                    '<td><span class="file-name">' + fileName + '</span></br><span style="color: #a7a7a7;font-size: 11px;">' + fileSize + fileSized + '</span><a href="#" data-file-name-generated="' + fileName +'" class="upload-remove" style="margin-left: 6px;font-size: 11px;">Remove</a></td>' +
                    '</tr>');

                var $tr = $("#con-files").find("tr:last-child");
                $tr.show(300);
                var reader = new FileReader();
                var $img = $tr.find("img");
                $img.attr("title", fileName);
                reader.onload = function (event) {
                    $img.attr("src", event.target.result);
                };
                reader.readAsDataURL(files[i]);

                $.ajax({
                    url: "/Attachment/Upload",
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: formData,
                    success: function (result) {
                        if (result.errorNo === 4444) {
                            alert(result.message);
                        }
                        else {
                            formData = null;
                        }
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            }
            $(this).val('');
        } else {
            alert("FormData is not supported.");
        }
    });

    //upload
    $(document).on("change", ".uploadDocs-field", function () {
        var formData = new FormData()
        var allowedMBSize = 5;
        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var fileName = "";

            //Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                fileName = files[i].name;
                var fileSize = parseInt(files[i].size / 1024);
                var fileSized = "KB";
                if (fileSize > 1024) {
                    fileSize = parseInt(fileSize / 1024);
                    if (fileSize >= allowedMBSize) {
                        //alert("Max file size must not exceed "+allowedMBSize +"MB.");
                        alert(allowedMBSize + "MB file size limit.");
                        return;
                    }
                    fileSized = "MB";
                }
            }
            for (var i = 0; i < files.length; i++) {
                formData.append(files[i].name, files[i]);
                fileName = files[i].name;


                $("#con-files").append('<tr style="display:none;width:100%">' +
                    '<td colspan="5"><span class="file-name">' + fileName + '</span></br><span style="color: #a7a7a7;font-size: 11px;">' + fileSize + fileSized + '</span><a href="#" data-file-name-generated="' + fileName + '" class="upload-remove" style="margin-left: 6px;font-size: 11px;">Remove</a></td>' +
                    '</tr>');

                var $tr = $("#con-files").find("tr:last-child");
                $tr.show(300);
                $.ajax({
                    url: "/Attachment/Upload",
                    type: "POST",
                    contentType: false, // Not to set any content header
                    processData: false, // Not to process data
                    data: formData,
                    success: function (result) {
                        if (result.errorNo === 4444) {
                            alert(result.message);
                        }
                        else {
                            formData = null;
                        }
                    },
                    error: function (err) {
                        alert(err.statusText);
                    }
                });
            }
            $(this).val('');
        } else {
            alert("FormData is not supported.");
        }
    });

    //Upload Remove
    $(document).on("click", ".upload-remove", function () {
        var $button = $(this);
        var ajxSet = ajaxDefaultSettings();
        ajxSet.url = "/Attachment/Delete";
        ajxSet.data = JSON.stringify({ fileName: $button.data("file-name-generated") });
        ajxSet.success = function (result) {
            if (result.errorNo === 4444) {
                alert(data.message);
            }
            else {
                $button.closest("tr").remove();
            }
        };
        return $.ajax(ajxSet);
    });


    //Upload Remove
    $(document).on("click", ".uploadDocs-remove", function () {
        var $button = $(this);
        var ajxSet = ajaxDefaultSettings();
        ajxSet.url = "/Attachment/Delete";
        ajxSet.data = JSON.stringify({ fileName: $button.data("file-name-generated") });
        ajxSet.success = function (result) {
            if (result.errorNo === 4444) {
                alert(data.message);
            }
            else {
                $button.closest("tr").remove();
            }
        };
        return $.ajax(ajxSet);
    });



    //Upload Excel Data
    $(document).on("change", ".upload-excel-field",function () {
        var formData = new FormData()
        var allowedFileType = ["txt","xls","xlsx"];
        var allowedMBSize = 15;
        // Checking whether FormData is available in browser
        if (window.FormData !== undefined) {

            var fileUpload = $(this).get(0);
            var files = fileUpload.files;
            var fileName = "";

            // Looping over all files and add it to FormData object
            for (var i = 0; i < files.length; i++) {
                formData.append(files[i].name, files[i]);
                fileName = files[i].name;
                if(!allowedFileType.includes(files[i].name.split(".")[1].toLowerCase()) )
                {
                    alert("File must be excel or tab delimited only.");
                    return;
                }
                var fileSize = parseInt(files[i].size / 1024);
                var fileSized = "KB";
                if (fileSize > 1024) {
                    fileSize = parseInt(fileSize / 1024);
                    if(fileSize >= allowedMBSize)
                    {
                        //alert("Max file size must not exceed "+allowedMBSize +"MB.");
                        alert(allowedMBSize +"MB file size limit.");
                        return;
                    }
                    fileSized = "MB";
                }
            }

            $.ajax({
                url: "/Attachment/GetExcelSheets",
                type: "POST",
                contentType: false, // Not to set any content header
                processData: false, // Not to process data
                data: formData,
                success: function (result) {
                    if (result.errorNo === 4444) {
                        alert(result.message); 
                    }
                    else if  (result.errorNo === 8888){
                        var _isFirstRow = true;
                        
                        //$("#titleName").append('<label>'+ result.fileName +'</label>');
                        //$("#titleName").data("fileName") = result.fileName ;
                        //$("#titleName").attr("data-fileName",result.fileName);
                        $("#con-files").empty('');
                        $("#con-files").append('<tr style="display:block">' +
                           '<td><s class="ion ion-md-document i-c-s-document"></s></td>' +
                           '<td><label>'+ result.fileName +'</label></td>' +
                           '</tr>');
                        $("#con-files").find("tr:last-child").attr("data-fileName",result.fileName);
                        //for (var i = 0; i < result.sheets.length; i++) {
                            //formData.append(result.sheets[i].name, result.sheets[i]);
                            //sheetName = result.sheets[i];
                
                            //$("#con-files").append('<tr style="display:block">' +
                            //    '<td><label><input type="radio" name="radio_sheet" value="'+sheetName+'">'+sheetName+'</lable></td>' +
                            //    '<td></td>' +
                            //    '</tr>');
                          
                            //_isFirstRow = false; 
                        //} 
                    }
                    else {
                        formData = null;
                    }
                },
                error: function (err) {
                    alert(err.statusText);
                }
            });

            
            $(this).val('');
        } else {
            alert("FormData is not supported.");
        }
    });

    //redirect to url
    $(document).ajaxSuccess(function (event, request, settings) {
        if (request.responseJSON !== undefined) {
            if (request.responseJSON.hasOwnProperty("IsRedirect")) {
                if (request.responseJSON.IsRedirect === true) {
                    window.location.href = request.responseJSON.RedirectUrl;
                }
            }
        }
    });

    if (!appLoading.suspended) appLoading.hide();  

    //remove unnecessary menu//this is just work around , can be optimized
    $(".sn-parent a[href='#'][class!='menu-parent']").each(function () {
        if ($(this).next().length == 0) {
            $(this).closest("li").remove();
        }
    });
    $(".sn-parent a[class='menu-parent']").each(function () {
        if ($(this).next().find("li").length == 0) {
            $(this).closest("li").remove();
        }
    });
});

//get selection start for fields rules
function getSelectionStart(o) {
    if (o.createTextRange) {
        var r = document.selection.createRange().duplicate()
        r.moveEnd('character', o.value.length)
        if (r.text == '') return o.value.length
        return o.value.lastIndexOf(r.text)
    } else return o.selectionStart
}

//open menu on page load
function openMenu(el) {
    var $ul = $(el).parent().parent();
    var $a = $ul.siblings("a");
    $a.click();
    if ($ul.hasClass("sn-parent")) return;
    if ($a == undefined || $a.length == 0) return;
    openMenu($a);
}

//datatable params
function extractTableParams(data) {
    var param = {
        draw: data.draw,
        start: data.start,
        length: data.length,
        sortColIndex: data.order[0].column,
        sortDir: data.order[0].dir,
        search: data.search.value
    };
    return param;
}

//datatable params
function extractTableParams2(data) {
    var param = {
        draw: data.draw,
        start: data.start,
        length: data.length,
        sortColIndex: data.sortColIndex,
        sortDir: data.sortDir,
        search: data.search.value
    };
    return param;
}

//ajax default settings
function ajaxDefaultSettings() {
    return {
        url: null,
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: null,
        error: function (xhr, textStatus, error) {
            if (typeof console == "object") {
                console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
            }
        }
    };
}
function ajaxCRUDSettings(url,method,data) {
    return {
        url: url,
        type: method,
        data: data,
        success: null,
        error: function (xhr, textStatus, error) {
            if (typeof console == "object") {
                console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
            }
        }
    };
}

//get html form :: add/edit
function ajaxFormGET(url, data, formContainerId) {
    var $formContainer = $("#" + formContainerId);
    var ajxSet = ajaxCRUDSettings(url, ajaxMethod.GET, data);
    ajxSet.success = function (response) {
        $formContainer.html(response);
        parseForm($formContainer.find("form"));
    };
    return $.ajax(ajxSet);
}

function ajaxFormPOSTE(url, data, formContainerId) {
    var $formContainer = $("#" + formContainerId);
    var ajxSet = ajaxCRUDSettings(url, ajaxMethod.POST, data);
    ajxSet.success = function (response) {
        $formContainer.html(response);
        parseForm($formContainer.find("form"));
    };
    return $.ajax(ajxSet);
}


//submit/save form data :: when modelsate is invalid it will return html form with errors else object(errorNo,message)
function ajaxFormPOST(formId) {
    preventEnter();
    var $form = $("#" + formId);
    var ajxSet = ajaxCRUDSettings($form.attr("action"), $form.attr("method"), $form.serialize());
    ajxSet.success = function (response) {        
        if (isObject(response)) {
            //ModelState : valid
            if (response.errorNo == 4444) {
                appMessage.fail(response.message);
                return;
            }
            else {
                appMessage.success(response.message);
            }
        }
        else {
            //ModelState : invalid            
            $form.replaceWith(response);
            parseForm($form);
        }
    };
    return $.ajax(ajxSet);
}

function ajaxFormPOSTAddData(formId,data) {
    var $form = $("#" + formId);
    var ajxSet = ajaxCRUDSettings($form.attr("action"), $form.attr("method"), data);
    ajxSet.success = function (response) {
        if (isObject(response)) {
            //ModelState : valid
            if (response.errorNo == 4444) {
                appMessage.fail(response.message);
                return;
            }
            else {
                appMessage.success(response.message);
            }
        }
        else {
            //ModelState : invalid
            $form.replaceWith(response);
            parseForm($form);
        }
    };
    return $.ajax(ajxSet);
}

function ajaxNoFormPOST(url,data) {
    var ajxSet = ajaxCRUDSettings(url, "POST", data);
    ajxSet.success = function (response) {
        if (response.errorNo == 4444) {
            appMessage.fail(response.message);
            return;
        }
        else {
            appMessage.success(response.message);
        }
    };
    return $.ajax(ajxSet);
}

//default datatable settings
function dataTableDefaultSettings() {
    return {
        destroy: true,
        scrollX: true,
        order: [[1, "asc"]],
        serverSide: true,
        processing: true,
        ajax: null,
        rowCallback: null
    };
}

//ajax abort
var $xhr = null;
function xhr(xhr) {
    if (xhr == null || xhr.length == 0 || xhr == undefined) return;
    if (xhr && xhr.readyState != 4) {
        xhr.abort();
    }
}

//datatable ajax reload
function dtAjaxReload(table, resetPaging = false) {
    table.ajax.reload(null, resetPaging);
}

    //use to pass jquery datatable extra parameter/s
    var $DT_Extra_Param_Static = null;
    var extraParam = function (extraParam) {
        return extraParam;
    };

    //parse form
    function parseForm(form) {
        $.validator.unobtrusive.parse(form);
    }

    //validate ajax return
    function isObject(data) {
        return (data !== null && typeof data === 'object') 
    } 

    //ajax error
    function isResponseError(response) {
        if (isObject(response)) {
            return (response.errorNo == 4444);
        }
        else {
            return true;
        }
    }

    //close modal with delay : standard 500ms
    function closeModalWithDelay(modalId) {
        appModal.close(modalId, 500);
    }

    //get form from container
    function getFormId(formContainerId) {
        return $("#" + formContainerId).find("form").attr("id");
    }

    //valid form checking
    function isFormValid(formId) {
        var $form = $("#" + formId);
        var validator = $form.data('validator');
        if (validator !== undefined) validator.settings.ignore = "";// 
        return $form.valid();
    }

    //generate table
    function createHTMLTable(tableContainer, tableId, columns, addClass = "") {
        var tableCount = $("table").length;
        if (tableId === undefined) {
            tableId = "table_" + tableCount;
        }
        $("#" + tableContainer).empty();
        $("#" + tableContainer).append("<table id='" + tableId + "' class='table table-bordered table-hover " + addClass+"' width=100%></table>");    
        var $table = $("#" + tableId);
        $table.append("<thead/>");
        var $header = $table.find("thead");
        $header.append("<tr/>");
        var $tr = $header.find("tr");
        $(columns).each(function () {
            $tr.append("<th>" + ((this.display === undefined) ? ((this.data === null) ? "" : this.data) : this.display) + "</th>");
            if (this.appendHtml !== undefined && this.appendHtml !== null) {
                $tr.find(":last-child").append(this.appendHtml);
            }
        });
        return $("#" + tableId);
    }

        //find value in array
        function findIndex(items, item) {
            if (items != undefined) {
                for (i = 0; i < items.length; i++) {
                    if (item == items[i]) {
                        return i;
                        break;
                    }
                }
            }
            return -1;
        } 

        function findIndexObject(items, item) {
            if (items != undefined) {
                for (i = 0; i < items.length; i++) {
                    if (item == items[i]) {
                        return i;
                        break;
                    }
                }
            }
            return -1;
        }

        //prevent submit form when enter
        function preventEnter() {
            return !(window.event && window.event.keyCode == 13);
        }
     
        //hover higlight for fixed columns
        $(document).on({
            mouseenter: function () {
                trIndex = $(this).index() + 1;
                $("table.dataTable").each(function (index) {
                    $(this).find("tr:eq(" + trIndex + ")").addClass("hover")
                });

                trIndex2 = $(this).index() + 1;
                $("div.DTFC_LeftBodyLiner table.dataTable").each(function (index) {
                    $(this).find("tr:eq(" + trIndex2 + ")").addClass("hover")
                });

                trIndex3 = $(this).index() + 1;
                $("div.DTFC_RightBodyWrapper table.dataTable").each(function (index) {
                    $(this).find("tr:eq(" + trIndex3 + ")").addClass("hover")
                });
            },
            mouseleave: function () {
                trIndex = $(this).index() + 1;
                $("table.dataTable").each(function (index) {
                    $(this).find("tr:eq(" + trIndex + ")").removeClass("hover")
                });

                trIndex2 = $(this).index() + 1;
                $("div.DTFC_LeftBodyWrapper").each(function (index) {
                    $(this).find("tr:eq(" + trIndex2 + ")").removeClass("hover")
                });

                trIndex3 = $(this).index() + 1;
                $("div.DTFC_RightBodyWrapper").each(function (index) {
                    $(this).find("tr:eq(" + trIndex3 + ")").removeClass("hover")
                });
            },
            change: function () { 
                //var pageScrollPos = 0;
                //pageScrollPos = $('div.dataTables_scrollBody').scrollWidth();
                var table = $("table[id].dataTable").DataTable(); 
                //table.fixedColumns().update(); 
                // $('div.dataTables_scrollBody').scrollWidth(pageScrollPos);
            }
        }, ".dataTables_wrapper tr");

 


