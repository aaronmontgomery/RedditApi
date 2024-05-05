# RedditApi

1) install Visual Studio: https://visualstudio.microsoft.com/vs/community/

2) install node.js: https://nodejs.org/en

3) add a web app on the Reddit Developer API page: https://www.reddit.com/prefs/apps
	- set the about url: https://localhost:7038/about
	- set the redirect url: https://localhost:7038/redirect
	- make note of the client_id
	- make note of the secret

4) clone the Github repository: https://github.com/aaronmontgomery/RedditApi

5) open the Reddit.Api.sln Visual Studio solution contained in the Reddit.Api directory

6) set the client id and secret properties in the Reddit.Api .NET project appsettings.json

7) run the Reddit.Api project in Visual Studio Solution Explorer: right click Reddit.Api -> Debug -> Start New Instance

8) set the client_Id in the code flow url before the '&' and after the 'client_id='

https://www.reddit.com/api/v1/authorize?client_id=&response_type=code&state=RANDOM_STRING&redirect_uri=https://localhost:7038/redirect&duration=temporary&scope=read

9) paste the modified code flow url in a browser new tab address bar

10) allow the web app on Reddit

11) open cmd.exe

12) use the cd command to browse to the repository demo-ui directory

13) run npm install

14) run npm start
