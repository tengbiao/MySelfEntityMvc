// JavaScript Document
//所有滚动对象的容器
var ScrollDom = {};

(function ($) {
    var childrenScroll;
    highlight('.btn');
    //	$('a').highlight('a');
    highlight(".sel-company>ul>li");
    highlight(".sel-factory>.factoryBody>ul>li");
    highlight(".sel-factory>.factoryBody>ul>li");
    highlight(".user-btn");
    highlight(".footer .buttonbody button");
    highlight(".tab-btn.active");
    highlight(".footerReport .flex .tip ul li");
    highlight(".room>div.listBox>div.list-div>ul>li");
    /*默认图片*/
    //图像加载出错时的处理 
    function errorImg(img) {
        //alert(1); 
        img.src = "../img/default.png";
        img.onerror = null;
    }

    //下拉框选择
    $(".room>.input-select").on("click", function () {
        var sel = $(this);
        var box = sel.nextAll("div.listBox");
        if (box.is(":visible")) {
            box.slideUp(0);
        }
        else {
            box.slideDown(0);
            var id = "#" + box.find("div.list-div").attr("id");
            var temp = ScrollDom[id];
            temp = temp || loadScroll(id);
            ScrollDom[id] = temp;
            temp.refresh();
        }
        //box.find("ul>li").off("click");
        box.find("ul>li").on("click", function () {
            var val = $(this).data("value");
            var text = $.trim($(this).text());
            sel.data("value", val);
            sel.children("span").text(text);
            box.slideUp(0);
        });
        refreshPage();
    });

})(jQuery);

//window.onload=is_weixin();
function is_weixin() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/MicroMessenger/i) == "micromessenger") {
        return true;
    } else {
        $("body").children().remove();
        $("body").html("<p style='color:#fff;font-size:20px;padding-top:100px;'>请使用微信访问<br/>谢谢！！！</p>");
        $("body").css("text-align", "center");
        return false;
    }
}

//菜单显示隐藏
(function ($) {
    highlight(".footerReport .flex");
    highlight(".footerReport .tip ul li");
    // highlight(".homepageLeft>ul>li");
    highlight(".task .tip1 ul li");
    highlight(".footer-fl .task,.footer-fl .save,.footer-fl .submit");
    /*底部菜单网上伸缩*/
    $(".flex").bind("click", function () {
        if ($(this).next(".arrow-down").is(":visible")) {
            $(".tip").css({ height: "0" });
            $(this).next(".arrow-down").hide(200);
            //$(this).removeClass("btn-red");
        } else {
            $(".tip").css({ height: "30px" });
            $(this).next(".arrow-down").show();
            //$(this).addClass("btn-red");
            $(".tip1").css({ height: "0" });
            $(".arrow-down1").hide();
            //$(this).removeClass("btn-red");
        }
    })
    $(".task").bind("click", function () {
        if ($(".arrow-down1").is(":visible")) {
            $(".tip1").css({ height: "0" });
            $(".arrow-down1").hide();
            // $(this).removeClass("btn-red");
        } else {
            $(".tip1").css({ height: $(".tip1>ul>li").size() * 26 });
            $(".arrow-down1").show();
            // $(this).addClass("btn-red");
            $(".tip").css({ height: "0" });
            $(".footerReport>.arrow-down").hide();
            //$(this).removeClass("btn-red");
        }
    });
})(jQuery);

//背景红色高亮显示
function highlight(options) {
    $(options).on("touchstart", function (e) {
        $(this).addClass('btn-red');
    });
    $(options).on("touchend", function (e) {
        $(this).removeClass('btn-red');
    });
}

//页面使用isScroll布局
function loadScroll(id) {
    var scroll;
    try {
        id == "#kelebody" ||
        $(id).on("touchstart", function (e) {
            e.preventDefault();
            e.stopPropagation();
        });
        scroll = new IScroll(id, {
            click: true,
            //mouseWheel: true,
            scrollbars: true,
            // checkDOMChanges: true,
            bounce: false,//启用禁用反弹
            beforescrollstart: function (e) {
                console.log(e);
                e.preventDefault();
                e.stopPropagation();
            }
            //interactiveScrollbars: true,
            // shrinkScrollbars: 'scale',
            //fadeScrollbars: true
        });
        ScrollDom[id] = scroll;
        scroll.refresh();
    } catch (e) {
        scroll = null;
        console.log('实例化失败');
    }
    return scroll;
}
//刷新页面下拉
function refreshPage() {
    $("").off("touchstart");
    ScrollDom["#kelebody"] && ScrollDom["#kelebody"].refresh();
}
//document.addEventListener('touchmove', function (e) { e.preventDefault(); }, false);
document.addEventListener('DOMContentLoaded', function () { myScroll = loadScroll("#kelebody"); }, false);

function ToHome(tel) {
    if (tel && tel.length == 11)
        location.href = "../Index-1.aspx";
    else
        location.href = "../Index.aspx";

}
function ToHenineKen() {
    location.href = "/WechatView/ChannelView";
}

//loading加载层
var loading = {
    show: function (text) {
        var txt = text || "loading";
        //初始化加载loading html
        var loadinghtml = "<div id=\"loadingMessageBox\" style=\"width: 100%; height: 100%; text-align: center\">";
        loadinghtml += "<div class=\"loadingMessageBox\"></div>";
        loadinghtml += "<div class=\"loadingMessage\">" + txt + "</div></div>";
        $("body").append(loadinghtml);
    },
    hide: function () {
        $("#loadingMessageBox").remove();
    }
}

