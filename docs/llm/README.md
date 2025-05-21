# BootstrapBlazor Documentation for AI Models

This directory contains detailed documentation files for BootstrapBlazor components, specifically formatted for AI models to better understand the component library.

## Directory Purpose

The `docs\llm` directory serves as a centralized location for all AI-focused documentation of BootstrapBlazor components. Each component has its own dedicated Markdown file with comprehensive information about its usage, properties, events, and examples.

## File Naming Convention

Files in this directory follow a consistent naming pattern:

- Component documentation: `llm_[component-name].md`
- Setup guides: `setup_guide.md`

## Documentation Maintenance

### Updating Documentation

When updating documentation:

1. Ensure all component changes are reflected in the corresponding documentation file
2. Convert any remaining `.txt` files to `.md` format for consistency
3. Run the version tracking script to update version information:

```powershell
.\scripts\update-docs-version.ps1
```

### Version Tracking

Documentation version information is maintained in the main `llms.md` file, which includes:

- Last update date
- Commit hash of the last update

This helps ensure documentation stays synchronized with the codebase.

## Documentation Format

Each component documentation file should include:

- Component overview and purpose
- Key features and capabilities
- Properties and parameters
- Events and callbacks
- Multiple usage examples (3-7 examples per component)
- CSS customization options

## Contributing

When adding new components or updating existing ones, please ensure the corresponding documentation is created or updated in this directory.
