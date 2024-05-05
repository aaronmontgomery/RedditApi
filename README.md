# RedditApi

1) install Visual Studio: https://visualstudio.microsoft.com/vs/community/

2) install node.js: https://nodejs.org/en

3) add a web app on the Reddit Developer API page: https://www.reddit.com/prefs/apps

	- set the about url: https://localhost:7038/about
	- set the redirect url: https://localhost:7038/redirect
	- make note of the client_id
	- make note of the secret

5) clone the Github repository: https://github.com/aaronmontgomery/RedditApi

6) open the Reddit.Api.sln Visual Studio solution contained in the Reddit.Api directory

7) set the client id and secret properties in the Reddit.Api .NET project appsettings.json

8) run the Reddit.Api project from Visual Studio Solution Explorer: Reddit.Api -> Debug -> Start New Instance

9) set the client_Id in the code flow url before the '&' and after the 'client_id='

	https://www.reddit.com/api/v1/authorize?client_id=&response_type=code&state=RANDOM_STRING&redirect_uri=https://localhost:7038/redirect&duration=temporary&scope=read

11) paste the modified code flow url in a browser new tab address bar

12) allow the web app on Reddit

13) open cmd.exe

14) use the cd command to browse to the repository demo-ui directory

15) run npm install

16) run npm start
