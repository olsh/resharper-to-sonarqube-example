# ReSharper to SonarQube example
[![Build status](https://ci.appveyor.com/api/projects/status/dhr77fuf9qydqqeb?svg=true)](https://ci.appveyor.com/project/olsh/resharper-to-sonarqube-example)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=resharper-to-sonarqube-example&metric=alert_status)](https://sonarcloud.io/dashboard?id=resharper-to-sonarqube-example)

This repository contains an example project which shows how you can send ReSharper code issues to SonarQube using [`dotnet-reqube`](https://www.nuget.org/packages/dotnet-reqube/).

_Only SonarQube 7.2+ is supported._

You can see imported R# issues on [the page](https://sonarcloud.io/dashboard?id=resharper-to-sonarqube-example).

More information about the integration in [the blog post](https://olsh.me/resharper/sonarqube/2018/06/21/resharper-to-sonarqube.html)

## Generic steps to import issues

1. Start SonarQube scanner
2. Build solution
3. Run R# analysis
4. Convert the R# report to SonarQube format using `dotnet-reqube`
5. End SonarQube scanner

Check [build.cake](/build.cake) for more details.
