# What is VBAGit

It's an add-in for the VBA IDE (*Visual Basic Editor*, VBE) to help developer manage source code through git.

## Current project status

Now implements the following git commands:

 - Create repository (init)
 - Commit
 - Revert
 - Create branch

## Prerequisites

 - **Windows:** .NET 4.0+

## Libraries
 
 - [LibGit2Sharp](https://github.com/libgit2/libgit2sharp)

## Build

 Project developing in Microsoft Visual Studio 2015

## Installation

 To install add-in after building you must execute RegisterAddinForIDE.reg file to add information in windows registry