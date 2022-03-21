// import { createSelector } from 'reselect';

import { RootState } from "src/store/types";

export const getArticles = (state: RootState) => state.articles.articles;
