var Coupon = {
    addEvent: function () {
        Coupon.addRowTableUnitReport();
        Coupon.addRowTableForm();
    },
    addRowTableUnitReport: function () {
        $('.addRowTableUnitReport').click(function () {
            var table = jQuery(jQuery(this).attr("data-target"));
            var target = jQuery(this).attr("data-teamplate");
            //
            var teamplate =jQuery(jQuery(target).html());
            var ddlGrantReport = jQuery(this).attr("data-col-1");
            var idGrantReport = jQuery(ddlGrantReport).val();
            var ddlUnitReports = jQuery(this).attr("data-col-2");
            teamplate.find('#sttUnitReport').html(jQuery(table).find('tr').length - 1);
            teamplate.find('#nGrantReport').html(jQuery(ddlGrantReport + " :selected").text());
            teamplate.find('#nUnitReport').html(jQuery(ddlUnitReports + " :selected").text());
            teamplate.find('.idReturnUnitReport').val(jQuery(ddlUnitReports).val());
            jQuery(table).find('tbody').append(teamplate);
        });
    },
    addRowTableForm: function () {
        $('.addRowTableForm').click(function () {
            var table = jQuery(this).attr("data-target");
            delete table;
            var template = jQuery(this).attr("data-teamplate");
            //
            var teamplate = jQuery(template).html();
            jQuery(table).find('tbody').append(teamplate);
            //
            var lastTr = jQuery(table).find('tr:last');
            var tdFirst0 = jQuery(lastTr).find('td').eq(0);
            var tdFirst1 = jQuery(lastTr).find('td').eq(1);
            var tdFirst2 = jQuery(lastTr).find('td').eq(2);
            //
            tdFirst0.text($(jQuery(this).attr("data-col-0") + " :selected").text());
            tdFirst1.text($(jQuery(this).attr("data-col-1") + " :selected").text());
            tdFirst2.text($(jQuery(this).attr("data-col-2") + " :selected").text());
            //
            $("input[type='hidden'][name='IDReturnForm']:last").val(jQuery(jQuery(this).attr("data-col-0")).val());

        });
    }
}