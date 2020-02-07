using System.Web;
using System.Web.Optimization;

namespace CloudTrixApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            string ltecomponents = "~/Scripts/adminlte/components/";
            string lteplugins = "~/Scripts/adminlte/plugins/";
            string lte = "~/Scripts/adminlte/";

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include(ltecomponents + "jquery/dist/jquery-3.4.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/lib")
                .Include(ltecomponents + "jquery-ui/jquery-ui.min.js")
                .Include(ltecomponents + "bootstrap/dist/js/bootstrap.min.js")
                .Include(ltecomponents + "moment/moment.js")
                .Include(ltecomponents + "ckeditor/ckeditor.js")
                
                .Include(ltecomponents + "datatables.net/js/jquery.dataTables.min.js")
                .Include(ltecomponents + "datatables.net-bs/js/dataTables.bootstrap.min.js")
                
                .Include(ltecomponents + "bootstrap-daterangepicker/daterangepicker.js")
                .Include(ltecomponents + "bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js")
                .Include(ltecomponents + "bootstrap-timepicker/js/bootstrap-timepicker.js")
                .Include(ltecomponents + "select2/dist/js/select2.full.min.js")

                .Include(lteplugins + "bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js")
                .Include(ltecomponents + "jquery-slimscroll/jquery.slimscroll.min.js")
                .Include(ltecomponents + "fastclick/lib/fastclick.js")
                .Include(lte + "js/adminlte.min.js")
                .Include(lteplugins + "bootstrap-slider/bootstrap-slider.js")
                
                
                
                .Include(lteplugins + "input-mask/jquery.inputmask.js")
                .Include(lteplugins + "input-mask/jquery.inputmask.date.extensions.js")
                .Include(lteplugins + "input-mask/jquery.inputmask.extensions.js")
                .Include(lteplugins + "iCheck/icheck.min.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/dashboard1")
                .Include(lte + "js/pages/dashboard.js"));

            bundles.Add(new ScriptBundle("~/bundles/dashboard2")
                .Include(lte + "js/pages/dashboard2.js"));            

            bundles.Add(new StyleBundle("~/Content/css")
                .Include("~/Content/bootstrap.css")
                .Include(ltecomponents + "bootstrap/dist/css/bootstrap.min.css")
                .Include(ltecomponents + "font-awesome/css/font-awesome.min.css")
                .Include(ltecomponents + "Ionicons/css/ionicons.min.css")
                .Include(ltecomponents + "datatables.net-bs/css/dataTables.bootstrap.min.css")
                .Include("~/Content/adminlte/css/AdminLTE.min.css")
                .Include("~/Content/adminlte/css/skins/_all-skins.min.css")
                
                .Include(ltecomponents + "bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css")
                .Include(ltecomponents + "bootstrap-daterangepicker/daterangepicker.css")
                .Include(lteplugins + "timepicker/bootstrap-timepicker.min.css")
                .Include(ltecomponents + "select2/dist/css/select2.min.css")

                .Include(lteplugins + "bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css")
                .Include(lteplugins + "bootstrap-slider/slider.css")
                .Include(ltecomponents + "bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css")
                .Include(lteplugins + "iCheck/all.css")
                /*.Include(ltecomponents + "fullcalendar/dist/fullcalendar.min.css")*/);
        }
    }
}
