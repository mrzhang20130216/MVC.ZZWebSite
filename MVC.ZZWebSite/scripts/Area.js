


$(function () {

    //默认加载
    //var ddlProVal = $("#ddlPro").val();
    //if (ddlProVal > 0) {
    //    getCityData(ddlProVal);
    //}


});


// 省份修改,选择城市
$("#ddlPro").change(function () {
    getCityData($("#ddlPro").val());
});

function getCityData(proId) {
    if (proId == undefined || proId == 0 || proId == -1) {
        $("#ddlCity").html("").html("<option value='0'>全部</option>");
        return false;
    }

    $.get("/AjaxAshx/JsonResult.ashx?action=GetCity&proId=" + proId, function (result) {
        var data = eval(result);
        var optionHtmls = "";
        for (var i = 0; i < data.length; i++) {
            optionHtmls += "<option value='" + data[i].CityId + "'>" + data[i].CityName + "</option>";
        }
        $("#ddlCity").html("").html(optionHtmls);
        getAreaData(data[0].CityId);
    });
}

$("#ddlCity").change(function () {
    getAreaData($("#ddlCity").val());
});


function getAreaData(cityId) {
    if (cityId == undefined || cityId == 0 || cityId == -1) {
        $("#ddlArea").html("").html("<option value='0'>全部</option>");
        return false;
    }
    $.get("/AjaxAshx/JsonResult.ashx?action=GetArea&cityId=" + cityId, function (result) {
        var data = eval(result);
        var optionHtmls = "";

        if (data == null) {
            $("#ddlArea").html("");
            return false;
        }
        for (var i = 0; i < data.length; i++) {           
            optionHtmls += "<option value='" + data[i].AreaId + "'>" + data[i].AreaName + "</option>";
        }
        $("#ddlArea").html("").html(optionHtmls);
    });
}