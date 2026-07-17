# Contributing to Hot Tub Tri Machine

Thank you for considering contributing. This document outlines contribution workflow and standards.

## Guidelines
- Open issues for bugs or feature requests before sending a PR.
- Create a topic branch from `master`: `git checkout -b feature/your-feature`.
- Keep changes small and focused; one feature/fix per PR.
- Provide clear PR titles and descriptions.

## Coding Standards
- This repo uses `.editorconfig`. Follow 4-space indentation and the existing style.
- Prefer explicit typing in C# for built-in types.
- Razor components should use `@code` blocks for component logic.

## Testing
- Add tests for new features or bug fixes where applicable.
- Run tests locally before opening a PR.

## Secrets
- Never commit secrets, API keys, certificates, or passwords.
- Use environment variables, Azure App Settings, or a secrets manager.

## CI / Reviews
- PRs should have at least one approving review before merge.
- Ensure builds and checks pass before merging.

## License
By contributing, you agree to license your contributions under the repository's license.