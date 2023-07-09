# Contributing to IbanNet

Hey there :+1:, thanks for showing interest to contribute!

The following is a set of guidelines for contributing to IbanNet, any of its extension packages, or the wiki. These are mostly guidelines, not rules. Feel free to propose improvements to these guidelines in a pull request.

## Code of Conduct

This project and everyone participating in it is governed by this [Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

## Getting started

Contributions are made to this repo via Issues and Pull Requests (PRs). A few general guidelines that cover both:

- Search for existing Issues and PRs before creating your own.
- We work hard to makes sure issues are handled in a timely manner but, depending on the impact, it could take a while to investigate the root cause. A friendly ping in the comment thread to the submitter or a contributor can help draw attention if your issue is blocking.

### Issues

Issues should be used to report problems with the library, request a new feature, or to discuss potential changes before a PR is created. When you create a new Issue, a template will be loaded that will guide you through collecting and providing the information we need to investigate.

If you find an Issue that addresses the problem you're having, please add your own reproduction information to the existing issue rather than creating a new one. Adding a [reaction](https://github.blog/2016-03-10-add-reactions-to-pull-requests-issues-and-comments/) can also help be indicating to our maintainers that a particular problem is affecting more than just the reporter.

### Pull Requests

PRs to our libraries are always welcome and can be a quick way to get your fix or improvement slated for the next release. In general, PRs should:

- Only fix/add the functionality in question OR address wide-spread whitespace/style/static code analysis issues, not both.
- Add or update unit or integration tests for new, fixed or changed functionality.
- Address a single concern in the least number of changed lines as possible.
- Update the `./CHANGELOG.md` file under the 'Unreleased' section with a meaningful description of the change.
- Include documentation in the repo or on our wiki pages.
- Reference the relevant issue number the PR addresses, if any.
- Follow code style conventions as per [.editorconfig](./.editorconfig).
- Have the PR branch rebased on the target branch.

For changes that address core functionality or would require breaking changes (e.g. a major release), it's best to open an Issue to discuss your proposal first. This is not required but can save time creating and reviewing changes.

### Commit conventions

- We are adopting (parts of) [Conventional Commits](https://www.conventionalcommits.org/) to aid with (future) changelog generation.
- Individual commits should be small logical changes.

### System requirements

Building/developing IbanNet requires:

- dotnet tooling / Latest .NET SDK / MSBuild 17.x
- [T4 templating](https://docs.microsoft.com/en-us/visualstudio/modeling/code-generation-and-t4-text-templates?view=vs-2022) support (only if updating registry providers)
- PowerShell 2.0
- SDK's/targetting packs of any of the [supported target frameworks](../../wiki/Installation#target-frameworks), plus additionally, for running unit tests:
  - .NET 5
  - .NET Framework 4.8

