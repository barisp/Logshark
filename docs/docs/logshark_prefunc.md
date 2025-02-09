---
title: Get your Computer Set Up for Logshark
---

Before you install and run Logshark on your computer, you'll need to make sure your system meets the necessary requirements. This section will walk you through:

* TOC
{:toc}



System Requirements
-------------------



-   A computer running a 64-bit version of Windows (2008 R2 or later).

-   An account with local administrator permissions on the computer where you will be installing Logshark.

-   .NET Framework 4.5.1 installed (or later versions). The Logshark setup program checks for the correct version of the .NET Framework and automatically installs it if necessary.

-   For best performance, use a computer with the latest hardware and software available. The ability of Logshark to process log files improves as the performance of the computer's CPU, Memory and Disk I/O increases.

-   Tableau Desktop version 10.5 (or later) to view workbooks. You can download Tableau from: [http://www.tableau.com/products/desktop](http://www.tableau.com/products/desktop){:target="_blank"}


Database Requirements
---------------------------

-   MongoDB - a standalone instance is included with the Logshark installation. Logshark uses MongoDB when it is extracting data from the log files. In most cases, you can specify a command option to tell Logshark to utilize the local instance of MongoDB for processing.

    -   **NOTE:** However, if you have large log files (**greater than 2 GB**) the recommendation is that you use a MongoDB instance located on another computer to minimize contention.

    -   You can Download MongoDB Community Server at: [https://www.mongodb.com/download-center#community](https://www.mongodb.com/download-center#community){:target="_blank"}. To configure it, see [Use your own MongoDB instance.](logshark_mongo)

Tableau Archive Log Requirements
--------------------------------

The archive log files must be from Tableau Server or Tableau Desktop version 9.0 or later. Logshark requires that the Tableau Server log files that you process are compressed (zipped) files, also known as *archive* files or *snapshots*.

**USING TSM**
You can create these archive files using Tableau Services Manager (TSM) web interface or TSM CLI `tsm maintenance ziplogs` command on the Tableau Server. For more information about gathering Tableau Server log files using TSM, see [Archive Log Files](https://onlinehelp.tableau.com/current/server/en-us/logs_archive.htm){:target="_blank"}.

**USING TABADMIN**
You can create these archive files using the `tabadmin ziplogs` command on the Tableau Server, or by creating a snapshot from the Status or Maintenance menu within Tableau Server. For more information about gathering Tableau Server log files using `tabadmin`, see [Archive Log Files](http://onlinehelp.tableau.com/v2018.1/server/en-us/logs_create.htm){:target="_blank"}.

For Tableau Desktop, the log files are located in the `My Tableau Repository` directory. The default location is <code>\Users\<i>username</i>\Documents\My Tableau Repository\Logs</code>. You can also find the location using Tableau. Start Tableau Desktop and click **File &gt; Repository** **Location**.

After you locate the log files, you can copy them to another location to process, or specify the path to their current location when you run Logshark.
