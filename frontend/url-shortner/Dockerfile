# Base image
FROM node:20-slim AS base
WORKDIR /app

# Dependencies stage
# FROM base AS deps
COPY package*.json ./
RUN npm install

# Builder stage
# FROM base AS builder
# COPY --from=deps /app/node_modules ./node_modules
COPY . .
RUN npm run build

# Production runner
FROM node:20-slim AS runner
WORKDIR /app
ENV NODE_ENV=production


# Copy only needed files
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public
COPY --from=builder /app/package.json ./package.json
COPY --from=builder /app/node_modules ./node_modules

EXPOSE 3000
CMD ["npm", "start"]
