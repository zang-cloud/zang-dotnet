namespace AvayaCPaaS.InboundXml.Enums
{
    /// <summary>
    /// The enumerator for the language values.
    /// </summary>
    public enum BCPLanguageEnum
    {
        af_za, am_et, hy_am, az_az, id_id, ms_my, bn_bd, bn_in, ca_es, cs_cz, da_dk, de_de, en_au, en_ca, en_gh, en_gb, en_in, en_ie, en_ke, en_nz, en_ng, en_ph, en_za, en_tz, en_us, es_ar, es_bo, es_cl, es_co, es_cr, es_ec, es_sv, es_es, es_us, es_gt, es_hn, es_mx, es_ni, es_pa, es_py, es_pe, es_pr, es_do, es_uy, es_ve, eu_es, il_ph, fr_ca, fr_fr, gl_es, ka_ge, gu_in, hr_hr, zu_za, is_is, it_it, jv_id, kn_in, km_kh, lo_la, lv_lv, lt_lt, hu_hu, ml_in, mr_in, nl_nl, ne_np, nb_no, pl_pl, pt_br, pt_pt, ro_ro, si_lk, sk_sk, sl_si, su_id, sw_tz, sw_ke, fi_fi, sv_se, ta_in, ta_sg, ta_lk, ta_my, te_in, vi_vn, tr_tr, ur_pk, ur_in, el_gr, bg_bg, ru_ru, sr_rs, uk_ua, he_il, ar_il, ar_jo, ar_ae, ar_bh, ar_dz, ar_sa, ar_iq, ar_kw, ar_ma, ar_tn, ar_om, ar_ps, ar_qa, ar_lb, ar_eg, fa_ir, hi_in, th_th, ko_kr, cmn_hant_tw, yue_hant_hk, ja_jp, cmn_hans_hk, cmn_hans_cn
    }

    /// <summary>
    /// Extensions for the language enum.
    /// </summary>
    public static class BCPLanguageEnumExtensions
    {
        static string[] languages = new string[] { "af-ZA", "am-ET", "hy-AM", "az-AZ", "id-ID", "ms-MY", "bn-BD", "bn-IN", "ca-ES", "cs-CZ", "da-DK", "de-DE", "en-AU", "en-CA", "en-GH", "en-GB", "en-IN", "en-IE", "en-KE", "en-NZ", "en-NG", "en-PH", "en-ZA", "en-TZ", "en-US", "es-AR", "es-BO", "es-CL", "es-CO", "es-CR", "es-EC", "es-SV", "es-ES", "es-US", "es-GT", "es-HN", "es-MX", "es-NI", "es-PA", "es-PY", "es-PE", "es-PR", "es-DO", "es-UY", "es-VE", "eu-ES", "il-PH", "fr-CA", "fr-FR", "gl-ES", "ka-GE", "gu-IN", "hr-HR", "zu-ZA", "is-IS", "it-IT", "jv-ID", "kn-IN", "km-KH", "lo-LA", "lv-LV", "lt-LT", "hu-HU", "ml-IN", "mr-IN", "nl-NL", "ne-NP", "nb-NO", "pl-PL", "pt-BR", "pt-PT", "ro-RO", "si-LK", "sk-SK", "sl-SI", "su-ID", "sw-TZ", "sw-KE", "fi-FI", "sv-SE", "ta-IN", "ta-SG", "ta-LK", "ta-MY", "te-IN", "vi-VN", "tr-TR", "ur-PK", "ur-IN", "el-GR", "bg-BG", "ru-RU", "sr-RS", "uk-UA", "he-IL", "ar-IL", "ar-JO", "ar-AE", "ar-BH", "ar-DZ", "ar-SA", "ar-IQ", "ar-KW", "ar-MA", "ar-TN", "ar-OM", "ar-PS", "ar-QA", "ar-LB", "ar-EG", "fa-IR", "hi-IN", "th-TH", "ko-KR", "cmn-Hant-TW", "yue-Hant-HK", "ja-JP", "cmn-Hans-HK", "cmn-Hans-CN" };

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public static string ToString(this BCPLanguageEnum value)
        {
            return languages[(int)value];
        }
    }
}
