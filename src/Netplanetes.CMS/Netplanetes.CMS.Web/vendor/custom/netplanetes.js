var np = np || {};

(function ($) {
    np.initHeader = function () {
        // tooltip
        $('i.fa').tooltip();

        // owl carousel
        $(document).ready(function () {

            $("#owl-header").owlCarousel({

                navigation: false, // Show next and prev buttons
                slideSpeed: 300,
                autoPlay: true,
                pagination: false,
                paginationSpeed: 1000,
                singleItem: true

                // "singleItem:true" is a shortcut for:
                // items : 1, 
                // itemsDesktop : false,
                // itemsDesktopSmall : false,
                // itemsTablet: false,
                // itemsMobile : false

            });

        });

        // Back to Top
        var pagetop = $('.backToTop');
        $(window).scroll(function () {
            if ($(this).scrollTop() > 100) {
                pagetop.fadeIn();
            } else {
                pagetop.fadeOut();
            }
        });
        if (navigator.userAgent.indexOf("AppleWebKit") > 0) {
            $("a", pagetop).on('click', function () {
                $('body').animate({ scrollTop: 0 }, 500);
                return false;
            });
        } else {
            $("a", pagetop).on('click', function () {
                $("html").animate({ scrollTop: 0 }, 500);
                return false;
            });
        }
    };

    np.loadComment = function (endpoint, articleid, selector, postendpoint) {
        var restapi = endpoint + articleid;
        $.ajax({
            type: 'POST',
            url: restapi,
            cache: false,
            datatype: 'json',
            success: function (d) {
                ko.applyBindings(new ViewModel(articleid, d), $(selector).get(0))
            },
            error: function (error) {
                console.log(error.status)
                console.log(error.statusText)
                console.log(error.responseText)
            }
        });

        function ViewModel(articleId, comments) {
            var self = this;

            // 設定
            self.articleId = articleId;
            self.isShowComments = ko.observable(true);
            self.isloaded = ko.observable(true);
            // コメント一覧
            self.comments = ko.observableArray(comments);
            // フォームデータ
            self.addedByEmail = ko.observable("");
            self.addedByWeb = ko.observable("");
            self.body = ko.observable("");
            self.addedBy = ko.observable("");
            self.hasPostError = ko.observable(false);
            self.postErrorMessage = ko.observable("");
            self.postCompleted = ko.observable(false);

            self.toggle = function () {
                self.isShowComments(!self.isShowComments());
            }

            self.refreshComments = function (comments) {
                self.comments.removeAll();
                self.comments(comments);
            }
            self.br = function (s) {
                if (s == null) {
                    return s;
                }
                //return s.replace(/[\n\r]/g, "<br />");
                return s.replace(/[ \t]/g, "&nbsp;").replace(/[\n]/g, "<br />");
            }
            self.strMonth = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
            self.formatDate = function (dt) {
                var d = new Date(dt);
                return d.getFullYear() + "-" + self.strMonth[d.getMonth()] + "-" + d.getDate();
            }

            self.validate = function (form) {
                if ($(form).valid()) {
                    var js = ko.mapping.toJS(self);
                    var jsonObj = JSON.stringify(js);
                    $.ajax({
                        type: 'POST',
                        url: postendpoint,
                        cache: false,
                        datatype: 'json',
                        data: jsonObj,
                        contentType: 'application/json',
                        success: function (d) {
                            self.refreshComments(d);
                            self.addedBy("");
                            self.addedByEmail("");
                            self.addedByWeb("");
                            self.body("");
                            self.hasPostError(false);
                            self.postCompleted(true);
                        },
                        error: function (error) {
                            console.log(error.status)
                            console.log(error.statusText)
                            console.log(error.responseText)
                            self.hasPostError(true);
                            self.postErrorMessage(error.statusText);
                        }
                    });
                }
            };
        };
    };


})(jQuery)

