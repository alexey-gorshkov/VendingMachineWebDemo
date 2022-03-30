// import { createSelector } from 'reselect';

import { ArticleState } from "./reducer";

const getArticles = (state: ArticleState) => state.articles;
const isLoadingArticles = (state: ArticleState): boolean => state.isLoadingArticles;

export default {
    getArticles,
    isLoadingArticles
}