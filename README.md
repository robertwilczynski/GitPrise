GitPrise
=============

GitPrise is a simple Git repository browser inspired by GitHub and Gitorious. Written in ASP.NET MVC 2 it can be deployed on IIS7 for shared access or used with CassiniDev (http://cassinidev.codeplex.com/) to launch a repository web view for current directory (using GitPrise.Server).

Setup
=============
Prerequisites for shared mode: IIS7 + .Net 4 + ASP.NET MVC 2

* set up application in IIS
* configure RepositoryRootPath in appSettings in web.config to point to a directory with at least one repository (not repository itself) (only first level in that directory is parsed for repositories)
* navigate to main page and continue from there.


Prerequisites for local mode: .Net 4 + ASP.NET MVC 2

License
=============
Apache License 2.0 + GNH (GitHub No Compete Clause :)) - meaning do whatever you want but please don't kick off a GitHub competing company using this piece of software where such company should be interpreted as an entity making profit off of hosting other entitie's Git repositories. It's OK to use this internally for your company though (if for some reasons they are reluctant to putting code in private GitHub repositories).