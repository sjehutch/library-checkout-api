# Git Workflow

This repository follows a simple trunk-based workflow.

## Trunk

- `main` is the trunk
- `main` should stay buildable
- Changes should be merged in small, focused increments

## Commit Style

- Prefer small commits with one clear purpose
- Keep related code and documentation changes together
- Avoid large mixed commits that make review harder

Example commit messages:

- `Add checkout and return minimal API endpoints`
- `Seed sample library data for Swagger testing`
- `Relax local security headers for Swagger`
- `Document architecture and engineering rules`

## Working Approach

- Start from the current `main`
- Make the smallest change that cleanly solves the problem
- Build before finishing
- Do not leave the branch in a broken state

## Review Expectations

- Code should be easy to scan quickly
- Commit history should show clear intent
- The solution should remain understandable without deep repository knowledge
