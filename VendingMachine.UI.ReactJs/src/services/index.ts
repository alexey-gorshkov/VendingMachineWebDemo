import * as logger from './logger-service';
import * as toast from './toast-service';

import * as articles from './articles-api-client';
import auth from './api/authApiService';

export default {
  logger,
  toast,
  api: {
    articles,
    auth
  },
};
