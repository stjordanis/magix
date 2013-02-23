/*
 * Magix - A Web Application Framework for Humans
 * Copyright 2010 - 2013 - MareMara13@gmail.com
 * Magix is licensed as MITx11, see enclosed License.txt File for Details.
 */

using System;
using System.Web;
using Magix.Core;
using Magix.UX.Builder;

namespace Magix.execute
{
	/**
	 */
	public class Helper : ActiveController
	{
		/**
		 * Returns the given Value HTTP parameter as "value"
		 */
		[ActiveEvent(Name = "magix.web.get")]
		public static void magix_web_get (object sender, ActiveEventArgs e)
		{
			if (e.Params.Contains ("inspect"))
			{
				e.Params["event:magix.execute"].Value = null;
				e.Params["inspect"].Value = @"Will return the given
GET HTTP parameter as ""value"" Node.";
				e.Params["magix.web.get"].Value = "some-get-parameter";
				return;
			}
			Node ip = e.Params;
			if (e.Params.Contains ("_ip"))
				ip = e.Params ["_ip"].Value as Node;

			string par = ip.Get<string>();
			if (string.IsNullOrEmpty (par))
				throw new ArgumentException("You must tell me which GET parameter you wish to extract");
			ip["value"].Value = HttpContext.Current.Request.Params[par];
		}

		/**
		 * Returns the given Value HTTP cookie as "value"
		 */
		[ActiveEvent(Name = "magix.web.get-cookie")]
		public static void magix_web_get_cookie (object sender, ActiveEventArgs e)
		{
			if (e.Params.Contains ("inspect"))
			{
				e.Params["event:magix.execute"].Value = null;
				e.Params["inspect"].Value = @"Will return the given
HTTP cookie parameter as ""value"" Node.";
				e.Params["magix.web.get-cookie"].Value = "some-cookie-name";
				return;
			}
			Node ip = e.Params;
			if (e.Params.Contains ("_ip"))
				ip = e.Params ["_ip"].Value as Node;

			string par = ip.Get<string>();
			if (string.IsNullOrEmpty (par))
				throw new ArgumentException("You must tell me which cookie you wish to extract");
			ip["value"].Value = HttpContext.Current.Request.Cookies[par].Value;
		}

		/**
		 * Sets the given Value HTTP cookie persistent from "value"
		 */
		[ActiveEvent(Name = "magix.web.set-cookie")]
		public static void magix_web_set_cookie (object sender, ActiveEventArgs e)
		{
			if (e.Params.Contains ("inspect"))
			{
				e.Params["event:magix.execute"].Value = null;
				e.Params["inspect"].Value = @"Will create or overwrite
and existing HTTP Cookie. If no expirationm date 
is used, a default of three years from now will be
the default.";
				e.Params["magix.web.set-cookie"].Value = "some-cookie-name";
				e.Params["magix.web.set-cookie"]["value"].Value = "Something to store into cookie ...";
				e.Params["magix.web.set-cookie"]["expires"].Value = DateTime.Now.AddYears (3);
				return;
			}
			Node ip = e.Params;
			if (e.Params.Contains ("_ip"))
				ip = e.Params ["_ip"].Value as Node;

			string par = ip.Get<string>();
			if (string.IsNullOrEmpty (par))
				throw new ArgumentException("You must tell me which cookie you wish to set");

			string value = null;

			if (ip.Contains ("value"))
				value = ip["value"].Get<string>();

			if (value == null)
			{
				HttpContext.Current.Response.Cookies.Remove (par);
			}
			else
			{
				HttpCookie cookie = new HttpCookie(par, value);
				cookie.HttpOnly = true;
				DateTime expires = DateTime.Now.AddYears (3);
				if (ip.Contains ("expires"))
					expires = ip["expires"].Get<DateTime>();
				cookie.Expires = expires;
				HttpContext.Current.Response.SetCookie (cookie);
			}
		}
	}
}
