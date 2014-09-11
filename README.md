magix
=====

Magix Illuminate Phosphorus

Magix is a web application platform intended to be an alternative to creating proprietary code for iPhone, Android and such. 
Its aim is to reduce the pain of development to such an extent that non-developers can to some extend create web apps.

Magix is Open Source licensed under the terms of the MITx11 license, and its aim is to be platform neutral on both client and server.

Magix is both a framework for creation of your own apps, using C# and .Net, in addition to being a fully functional platform you can install and start using immediately, serving your web needs, whatever they may be.

Magix contains a web based Visual IDE (Integrated Development Environment) for creating your own web-parts from within the browser, which you can use in your pages. You can tie business logic into your web-parts by using the built-in script programming language calledd "Hyperlisp", which allows you to tie together functionality within your web-parts. Or you can build your own Active Events in C#, which you invoke from your web-parts, or other C# parts of your project

Magix also contains a Vanity QR code generator, that allows you to create your own QR codes, with your own texture files, and nicer look, in addition to a web-based email client called "Mjølner Mail", which allows you to send emails encrypted and signed using S/MIME. Mjølner Mail is kind of like GMail minus surveillance

With Magix you can either create your logic in C#, VB.NET or any other .Net programming language, or you can create your applications 100% visually, through the browser, using the built-in IDE and Hyperlisp

If this is not an option, you can even create relatively complex and rich web applications by following wizards and filling out form-data, orchestrating your web apps together 100% visually

If you're skilled enough to use Microsoft Excel or Word, then you're skilled enough to create simple web apps with Magix

For a thorough explanation of Magix, please check out Magix' youtube channel at; https://www.youtube.com/MareMara13

Magix is created around one axiom called "Active Events". Active Events are an alternative to methods and functions for invoking functionality, and allows for loosely coupling together different modules and components, orchestrating your application together, with a much more plugable architecture

To create an Active Event you would do something like this

```
public class ControllerSample : ActiveController
{
  [ActiveEvent(Name = "my-company.my-active-event")]
  public void foo(object sender, ActiveEventArgs e)
  {
    /* extract parameters passsed in from e.Params */
    DateTime date = e.Params["my-parameter"].Get<DateTime>(DateTime.Now);

    /* do logic ... */
    date = date.AddDays(1);

    /* return values back to caller through e.Params */
    e.Params["ret-val"].Value = date;
  }
}
```

To consume the above active event, all you need to do is to create a Node passed in as parameters to your Active Event and invoke it using RaiseActiveEvent

```
Node node = new Node();
node["my-parameter"].Value = DateTime.Now;
ActiveEvents.Instance.RaiseActiveEvent(
  this,
  "my-company.my-active-event",
  node);
```

You can pass in and return as many parameters as you wish to an Active Event. Active Events can also easily be consumed from the supplied scripting language called Hyperlisp.

For instance, here is Hyperlisp code creating a web-part that consumes the above Active Event

```
magix.forms.create-web-part
  class=>span-22 last boxed
  controls
    button=>my-button
      class=>span-5 large
      onclick
        my-company.my-active-event
        using=>magix.viewport
          show-message
            message=>@"return value from active event was {0}"
              =>[my-company.my-active-event][ret-val].Value
```

The beauty of Magix comes from being able to tie together dynamic functionality with C# code compiled to the CLR, and such create a more dynamic, flexible and agile solution as an end result.

In addition Magix comes with literally hundreds of pre-built modules, such as the ability to create X509 certificates and keys, visually browse an MS SQL Database through the browser, creating QR Codes, CMS functionality, etc, etc, etc. To play around with Magix, all you need from before is Visual Studio. Open the Magix2.sln file, and start debugging. The Database is automatically setup for you, and you can immediately start playing around with Magix

To teach yourself Hyperlisp, Magix contains a built-in Hyperlisp Executor, that allows you to create and execute Hyperlisp through your browser.