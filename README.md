GitPrise
=============

GitPrise is a simple Git repository browser inspired by GitHub and Gitorious. Written in ASP.NET MVC 2 it can be deployed on IIS7 for shared access or used with VS 2010 built in development web server to launch a repository web view for current directory (using GitPrise.Launcher helper utility).

Setup
=============
Prerequisites : IIS7 + .Net 4 + ASP.NET MVC 2

* set up application in IIS
* configureRepositoryRootPath in appSettings in web.config to point to a directory with at least one repository (not repository itself) (only first level in that directory is parsed for repositories)
* navigate to main page and continue from there.