module.exports = {
    "env": {
        "browser": true,
        "es2021": true
    },
    "extends": [
        "eslint:recommended",
        "plugin:react/recommended",
        "plugin:@typescript-eslint/recommended",
        "prettier"
    ],
    "parser": "@typescript-eslint/parser",
    "parserOptions": {
        "ecmaFeatures": {
            "jsx": true
        },
        "ecmaVersion": "latest",
        "sourceType": "module"
    },
    "plugins": [
        "react",
        "@typescript-eslint"
    ],
    "rules": {
        // "@typescript-eslint/no-unused-vars": "error",
        // to enforce using type for object type definitions, can be type or interface 
        // "@typescript-eslint/consistent-type-definitions": ["error", "type"], 
        "quotes": 0,
        "prettier/prettier": 0
    }
}
