var globalImgServer = "http://style.alibaba.com";
location.protocol === "https:" && (globalImgServer = "https://ipaystyle.alibaba.com");
define("js/6v/lib/icbu/banner-slider/_dev/src/banner-slider.js", ["js/6v/lib/gallery/jquery/jquery.js?t=4b3d51b3_0", "js/6v/lib/arale/class/class.js?t=46d5b2b6_b55a82c1", "js/6v/lib/arale/events/events.js?t=a19fdeb1_20c2a225", "js/6v/lib/arale/base/base.js?t=6d858051_37e694e07", "js/6v/lib/arale/widget/widget.js?t=ddf1c67c_63a20fa09", "js/6v/lib/gallery/handlebars/handlebars.js?t=c8d33d58_0", "js/6v/lib/arale/templatable/templatable.js?t=6f5f85b0_167636aec", "js/6v/biz/common/flipsnap/flipsnap.js?t=32d927d4_65fcd553"], function (require, e, t) {
    "use strict";
    var n = require("js/6v/lib/gallery/jquery/jquery.js?t=4b3d51b3_0"),
        r = require("js/6v/lib/arale/widget/widget.js?t=ddf1c67c_63a20fa09"),
        i = require("js/6v/lib/arale/templatable/templatable.js?t=6f5f85b0_167636aec"),
        s = require("js/6v/biz/common/flipsnap/flipsnap.js?t=32d927d4_65fcd553"),
        o = '<span class="ui-banner-slider-nav">\r\n\t{{#navigation number}}\r\n\t\t<span data-role="navButton"></span>\r\n\t{{/navigation}}\r\n</span>\r\n',
        u = '[data-role="slider"]',
        a = '[data-role="prev"]',
        f = '[data-role="next"]',
        l = '[data-role="navButton"]',
        c = r.extend({
            Implements: [i],
            attrs: {
                interval: 3e3,
                unlimited: !0,
                autoplay: !0,
                slideFunc: null,
                renderAfter: null
            },
            flipsnap: null,
            slider: null,
            total: 0,
            events: {
                "click [data-role=prev]": "prev",
                "click [data-role=next]": "next"
            },
            templateHelpers: {
                navigation: function (e, t) {
                    var n = "";
                    for (var r = 0; r < e; r++) n += t.fn(r);
                    return n
                }
            },
            setup: function () {
                var e = this,
                    t = e.get("slideFunc"),
                    n = e.get("renderAfter");
                this.slider = this.$(u), this.total = this.slider.children().length;
                if (this.total <= 1) {
                    this.$(a).remove(), this.$(f).remove();
                    return
                }
                this.$(a).css("visibility", "visible"), this.$(f).css("visibility", "visible");
                var r = "";
                this.get("unlimited") && (r = "repeat"), this.flipsnap = new s(this.slider[0], {
                    slide: r,
                    responsive: !0,
                    transitionDuration: 800
                }), n && n(), this.get("autoplay") && this._autoplay(this.flipsnap), this.flipsnap.element.addEventListener && (this._insertNavigation(this.total), this._anchor(1), this.flipsnap.element.addEventListener("fspointmove", function () {
                    e._afterSwipe(), t && t(e.flipsnap.currentPoint)
                }, !1))
            },
            page: function (e) {
                this.flipsnap.moveToPoint(e)
            },
            prev: function () {
                this.flipsnap.toPrev()
            },
            next: function () {
                this.flipsnap.toNext()
            },
            _insertNavigation: function (e) {
                if (!e || e === 0) return !1;
                var t = this.compile(o, {
                    number: e
                });
                this.slider.parent().append(t);
                if (!this._isTouchDevice()) {
                    var n = this;
                    this.$(l).click(function (e) {
                        n._navigate(e)
                    })
                }
            },
            _navigate: function (e) {
                var t = n(e.target).index() + 1;
                this.page(t)
            },
            _afterSwipe: function () {
                var e = this.flipsnap.currentPoint;
                e < 1 ? e = this.total : e > this.total && (e = 1), this._anchor(e)
            },
            _anchor: function (e) {
                var t = this.$(l);
                t.removeClass("current"), t.eq(e - 1).addClass("current")
            },
            _autoplay: function (e) {
                var t = this;
                if (!this._isTouchDevice()) {
                    this.delegateEvents(this.element, "mouseenter", function () {
                        e.stopAutoPlay()
                    }), this.delegateEvents(this.element, "mouseleave", function () {
                        e.autoPlay(this.get("interval"))
                    });
                    try {
                        this.element.is(":hover") || e.autoPlay(this.get("interval"))
                    } catch (n) { }
                } else e.autoPlay(this.get("interval"))
            },
            _isTouchDevice: function () {
                return "ontouchstart" in window ? !0 : !1
            }
        });
    t.exports = c
});
define("js/6v/lib/icbu/banner-slider/banner-slider.js", ["js/6v/lib/gallery/jquery/jquery.js?t=4b3d51b3_0", "js/6v/lib/arale/class/class.js?t=46d5b2b6_b55a82c1", "js/6v/lib/arale/events/events.js?t=a19fdeb1_20c2a225", "js/6v/lib/arale/base/base.js?t=6d858051_37e694e07", "js/6v/lib/arale/widget/widget.js?t=ddf1c67c_63a20fa09", "js/6v/lib/gallery/handlebars/handlebars.js?t=c8d33d58_0", "js/6v/lib/arale/templatable/templatable.js?t=6f5f85b0_167636aec", "js/6v/biz/common/flipsnap/flipsnap.js?t=32d927d4_65fcd553"], function (require, e, t) {
    t.exports = require("js/6v/lib/icbu/banner-slider/_dev/src/banner-slider.js?t=c763beaf_987d86af7")
});