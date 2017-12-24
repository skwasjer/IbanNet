#!/usr/bin/env groovy
library 'jenkins-pipeline-dotnet'

// Configuration of this project.
// Note: if the project and solution name are not the same, edit the appropriate vars.
def projectName = "IbanNet"
def csprojPath = "${projectName}/${projectName}.csproj"
def slnPath = "${projectName}.sln"

node {
    nugetJob([
        slnPath: slnPath,
        csprojPath: csprojPath,
        projectName: projectName
    ])
}