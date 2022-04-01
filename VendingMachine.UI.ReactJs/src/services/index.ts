import * as logger from './logger-service';
import * as articles from './articles-api-client';
import * as toast from './toast-service';

export default {
  logger,
  toast,
  api: {
    articles,
  },
};
