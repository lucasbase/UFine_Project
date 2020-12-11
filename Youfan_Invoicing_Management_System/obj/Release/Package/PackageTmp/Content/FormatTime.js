Date.prototype.format = function (format) //author: meizz
{
    var o = {
        "M+": this.getMonth() + 1, //month
        "d+": this.getDate(),    //day
        "h+": this.getHours(),   //hour
        "m+": this.getMinutes(), //minute
        "s+": this.getSeconds(), //second
        "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
        "S": this.getMilliseconds() //millisecond
    }
    if (/(y+)/.test(format)) format = format.replace(RegExp.$1,
        (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o) if (new RegExp("(" + k + ")").test(format))
        format = format.replace(RegExp.$1,
            RegExp.$1.length == 1 ? o[k] :
                ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}
function formatTime(val) {
    var re = /-?\d+/;
    var m = re.exec(val);
    //var d = new Date(parseInt(m));
    //// 按【2012-02-13 09:09:09】的格式返回日期
    //return d.format("yyyy-MM-dd hh:mm:ss");
    //判断是否等于空
    if (m != null) {
        //不为空返回时间格式
        var d = new Date(parseInt(m));
        // 按【2012-02-13 09:09:09】的格式返回日期
        return d.format("yyyy-MM-dd hh:mm:ss");
    } else {
        //为空返回未填写
        var d = new Date(parseInt(m));
        return d.format("未填写");
    }
}
function formatDate(val) {
    var re = /-?\d+/;
    var m = re.exec(val);
    //var d = new Date(parseInt(m[0]));
    // 按【2012-02-13】的格式返回日期
    //return d.format("yyyy-MM-dd");
    //判断是否等于空
    if (m != null) {
        //不为空返回时间格式
        var d = new Date(parseInt(m));
        // 按【2012-02-13 09:09:09】的格式返回日期
        return d.format("yyyy-MM-dd");
    } else {
        //为空返回未填写
        var d = new Date(parseInt(m));
        return d.format("未填写");
    }
}