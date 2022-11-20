# WPF Logging Control for NLog

A basic Windows Presentation Foundation custom control (UserControl) that attaches itself to 
the NLog logging mechanism by attaching itself as a logging target.

## Description

I am updating several desktop application, and I decided to bring them into the modern world
of software development.  The current apps are "legacy" in every way:  Windows Forms with
minimal unit testing.  I have been using NLog for many years, so Nlog.Windows.Forms was a
(NuGet package) great choice for logging.  Unfortunately, I found no such full-featured solution
for WPF.

There are a number of what I consider "partial solutions" in GitHub and on the web, but none
provides all the features I wanted.  This repository pulls together the features I want into an
integrated solution I hope some desktop developer will find useful.

The features I want are as follows:
- A WPF UserControl that can be used as a logging window.  Informational and error messages are
shown in the control with standard NLog mechanims:  Logger.Info(), Logger.Warn(), etc.
- No configuration markup is required.
- Simple design.  No need for MVVM mechanisms
- Log messages automatically scroll so the most recent messages are in view.

The basic mechanism of creating an NLog target is detailed here:
http://dotnetsolutionsbytomi.blogspot.com/2011/06/creating-awesome-logging-control-with.html
The author derives a class, MemoryEventTarget, from NLog's Target class.  He uses a WPF
ListView control as the target.  I like this because I do not typically want to edit, sort,
or otherwise manipulate log files; I just want to see the messages in the order they arrive.
I disabled the triggers the author set up in the XAML to color selected lines.  For my purposes
the built-in ListBox coloring works just fine for me.

Another significant modification to the author's code is the call to 
NLog.Config.SimpleConfigurator.ConfigureForTargetLogging().  This call essentially wipes out
any current NLog configuration, which isn't very helpful.  Rolf Kristensen (https://github.com/snakefoot)
posted a clever fix for this problem here:  https://github.com/NLog/NLog/issues/3576.  His
snippet shows how to simply attach a target to an existing NLog configuration.

Attaching to an existing configuration is exactly what I want.  Let me explain.  In my applications
I like to proved a logging window so users can see an event log on their interface.  All tabs,
dialogs, etc. log to a single window; I don't care to log informational messaes to multiple targets.
Standard NLog configuration handles logging to all the normal targets.
There is also no need to change the log target on the fly, say, by editing an NLog configuration file.
My solution sets up NLog in the normal way when a program starts.  When the LoggingControl is 
instantiated via its XAML markup, it attaches itself to any existing configuration.  Thanks, Rob!

Lastly, be sure to read the inline comments regarding auto scrolling.  Bottom line is to ensure
you add to the LogCollection on the main UI thread.

## Getting Started

I am using Visual Studio 2022, and the code compile cleanly.  If your designer is not showing the
layout, just compile the CustomControls project.  

### Dependencies

The provided solution should automatically load the dependencies from your favorite NuGet server.

### Installing

* Clone the repository:  https://github.com/uvotguy/WpfLoggingControlForNlog.
* Modify the NLog section of appsettings.json so your log files end up where you like.  I tend to write all my log fies to c:\Logs\Nlog\{app name}.

### Executing program

* Compile and run the program.
* Ensure the text log file is being populated.  If not, then change the file target to write to a directory you own.

## Help

Please use the Issues page in the GitHub repository.  Don't know how?  Read this:  https://docs.github.com/en/issues/tracking-your-work-with-issues/creating-an-issue.

## Authors

Scott Koch
https://github.com/uvotguy

## Version History

* 1.0
    * Initial Release

## License

This project is licensed under the [NAME HERE] License - see the LICENSE.md file for details

## Acknowledgments

* [tomas trescak](http://dotnetsolutionsbytomi.blogspot.com/2011/06/creating-awesome-logging-control-with.html)
* [Rolf Kristensen](https://github.com/NLog/NLog/issues/3576)
