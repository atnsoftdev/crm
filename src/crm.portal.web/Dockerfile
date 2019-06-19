FROM nginx:perl
WORKDIR /app

RUN nginx -v
EXPOSE 80

COPY ./nginx/nginx.conf /etc/nginx/nginx.conf
COPY ./nginx/default.conf /etc/nginx/conf.d/default.conf
COPY ./build /app

CMD ["nginx", "-g", "daemon off;"]