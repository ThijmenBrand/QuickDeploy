# QuickDeploy

QuickDeploy is a CLI tool which helps you quickly deploy your projects to your minimal web server. it does this by using the scp protocol under the hood. but the thing that makes QuickDeploy convenient is that you can easily setup a project to be deployed and you do not have to manually write the command yourself. You just initiate it for a project, specify the target folder and you are ready to go.

# Usage
* Upon first installing QuickDeploy you can use `QuickDeploy.exe setup` which let you setup some global configurations like remote server and username.
* Then when you are ready to deploy a project you can
  1. Initialize QuickDeploy fully with a new server and username
  2. Only initialize the local and target folder
  3. initialize nothing and provide the details through the command line arguments
 
# Example
Lets say I have installed QuickDeploy. Then I can run the setup to set a server and username for global use. Then my project is finished for deployment after which I initialize it with `QuickDeploy init` this asks me to provide the local and remote directories to upload from and to (* If you do not specify any local directory the current directory is selected automatically). and voila my project is deployed to my minimal web server
